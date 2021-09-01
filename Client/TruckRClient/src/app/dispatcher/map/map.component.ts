import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { Map } from 'leaflet';
import * as L from 'leaflet';
import { LocationsService } from 'src/infrastructure/map/locations/services/locations.service';
import { Subscription } from 'rxjs';
import { UserLocation } from 'src/infrastructure/map/locations/models/user-location';
import { TruckrLeafletMapMarker } from 'src/infrastructure/map/map/models/truckr-leaflet-map-marker';
import { Truck } from './markers/truck';
import { Coordinates } from 'src/infrastructure/map/locations/models/coordinates';
import { DriverInfoMapPopupService, DriverPopupInfo } from 'src/infrastructure/map/map/services/driver-info-map-popup.service';
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { LoaderService } from 'src/infrastructure/common/loader.service';
import { NotificationService } from 'src/infrastructure/common/notification.service';
import { UserDetailsService } from 'src/infrastructure/methods/user-details/user-details.service';
import { TransportHubService } from 'src/infrastructure/assignments/transport-hub.service';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit {
  map: Map;

  private locationsConnectionSubscription: Subscription;
  private locationSharedSubscription!: Subscription;
  private userDisconnectedSubscription: Subscription;
  private driverInfoFetchedSubscription: Subscription;

  private transportConnectionSubscription: Subscription;
  private sharedConnectionSubscription: Subscription;
  private assignmentFinishedSubscription: Subscription;
  private assignmentTakenSubscription: Subscription;
  private assignmentFailedSubscription: Subscription;
  private assignmentExpiredSubscription: Subscription;

  private markers: Array<TruckrLeafletMapMarker>;

  driverInfoModalRef?: BsModalRef;
  currentDriverInfo: DriverPopupInfo;
  @ViewChild('driverInfoModal') driverInfoModal: TemplateRef<any>;
  clickedMarker: TruckrLeafletMapMarker;

  constructor(
    private locationsService: LocationsService,
    private driverPopupInfoService: DriverInfoMapPopupService,
    private modalService: BsModalService,
    private loader: LoaderService,
    private notifications: NotificationService,
    private userDetails: UserDetailsService,
    private transportHubService: TransportHubService) {
    this.markers = new Array<TruckrLeafletMapMarker>();

    this.driverInfoFetchedSubscription = this.driverPopupInfoService.$infoRetrieved.subscribe((info) => {
      this.currentDriverInfo = info;
      this.loader.hide();
      this.openModal();
    });

    this.locationsConnectionSubscription = this.locationsService.$connected.subscribe(connected => {
      if (connected) {
        this.locationsService.onLocationReceived();
        this.locationsService.onUserDisconnected();
      }
    });

    this.locationSharedSubscription = this.locationsService.$locationReceived.subscribe(location => {
      if (location) {
        this.addOrUpdateLocationMarker(location);
      }
    });

    this.userDisconnectedSubscription = this.locationsService.$userDisconnected.subscribe(userId => {
      if (userId) {
        this.userDetails.fetch(userId).subscribe(result => {
          if (result.successful) {
            this.notifications.showInfo('Kierowca ' + result.details.firstName + ' ' + result.details.lastName + ' rozłączył się.');
          } else {
            this.notifications.showInfo('Kierowca ' + userId + ' rozłączył się.');
          }

          let driverMarker = this.markers.find(x => x.id === userId);
          if (driverMarker) {
            driverMarker.setIcon(Truck.HighlightedRed);
            driverMarker.setPathColor(Truck.HightlightColorRed);
          }

          setTimeout(() => this.removeMarker(userId), 5000);
        });
      }
    });

    this.transportConnectionSubscription =
      this.transportHubService.$connected.subscribe(connected => {
        if (connected) {
          this.transportHubService.onAssignmentFinished();
          this.transportHubService.onAssignmentTaken();
          this.transportHubService.onAssignmentExpired();
          this.transportHubService.onAssignmentFailed();
        }
      });

    this.assignmentFinishedSubscription =
      this.transportHubService.assignmentFinished$.subscribe(ass => {
        if (ass) {
          this.notifications.showSuccess("Zlecenie " + ass.assignmentId + " zostało wykonane.");
        }
      });

    this.assignmentTakenSubscription =
      this.transportHubService.assignmentTaken$.subscribe(ass => {
        if (ass) {
          this.notifications.showInfo("Zadanie " + ass.assignmentId + " zostało podjęte.");
        }
      });

    this.assignmentExpiredSubscription =
      this.transportHubService.assignmentExpired$.subscribe(ass => {
        console.log(ass);
        if (ass) {
          this.notifications.showWarning("Minął czas na wykonanie zlecenia " + ass.assignmentId + ".")
        }
      });

    this.assignmentFailedSubscription =
      this.transportHubService.assignmentFailed$.subscribe(ass => {
        if (ass) {
          this.notifications.showError("Minął czas na wykonanie podjętego zlecenia " + ass.assignmentId + ".")
        }
      });
  }

  ngOnInit(): void {
    this.initConnection();

    var body = document.getElementsByTagName("body")[0];
    body.classList.add("map-page");

    setTimeout(() => this.initMap(), 10);
  };

  ngOnDestroy(): void {
    if (this.locationSharedSubscription)
      this.locationSharedSubscription.unsubscribe();

    if (this.locationsConnectionSubscription)
      this.locationsConnectionSubscription.unsubscribe();

    if (this.userDisconnectedSubscription)
      this.userDisconnectedSubscription.unsubscribe();

    if (this.driverInfoFetchedSubscription)
      this.driverInfoFetchedSubscription.unsubscribe();

    if (this.assignmentExpiredSubscription)
      this.assignmentExpiredSubscription.unsubscribe();

    if (this.assignmentFailedSubscription)
      this.assignmentFailedSubscription.unsubscribe();

    if (this.assignmentFinishedSubscription)
      this.assignmentFinishedSubscription.unsubscribe();

    if (this.assignmentTakenSubscription)
      this.assignmentTakenSubscription.unsubscribe();

    if (this.transportConnectionSubscription)
      this.transportConnectionSubscription.unsubscribe();

    if (this.sharedConnectionSubscription)
      this.sharedConnectionSubscription.unsubscribe();

    var body = document.getElementsByTagName("body")[0];
    body.classList.remove("map-page");

    this.destroyMap();
  };

  private initMap = (): void => {
    if (!this.map) {
      this.map = L.map('map', {
        center: [52.9105956100339, 18.92453255121571],
        zoom: 7
      });

      const tiles = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 18,
        minZoom: 6,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
      });

      tiles.addTo(this.map);
    }
  };

  private destroyMap(): void {
    this.map.remove();
    this.map = null;
  }

  private initConnection = (): void => {
    if (this.locationsService.isDisconnected) {
      this.locationsService.connect();
    } else if (this.locationsService.isDisconnecting) {
      setTimeout(() => this.locationsService.connect(), 100);
    }

    if (this.transportHubService.isDisconnected) {
      this.transportHubService.connect();
    } else if (this.transportHubService.isDisconnecting) {
      setTimeout(() => this.transportHubService.connect(), 100);
    }
  };

  private addOrUpdateLocationMarker = (location: UserLocation): void => {
    let marker = this.markers.find(x => x.id == location.userId);

    if (marker) {
      marker.marker.setLatLng(L.latLng(location.coordinates.latitude, location.coordinates.longitude));
      this.drawPath(marker, marker.currentLocation, location.coordinates);
      marker.currentLocation = location.coordinates;
    } else {
      this.addMarker(location);
    }
  };

  private addMarker = (location: UserLocation): void => {
    let newMarker = new TruckrLeafletMapMarker();

    newMarker.id = location.userId;
    newMarker.currentLocation = location.coordinates;
    newMarker.marker = L.marker(L.latLng(location.coordinates.latitude, location.coordinates.longitude));
    newMarker.setIcon(Truck.Normal);
    newMarker.currentColor = Truck.NormalColor;

    newMarker.marker.on("click", (e) => {

      e.target.setIcon(Truck.Highlighted);
      newMarker.currentColor = Truck.HightlightColor;
      newMarker.setPathColor(Truck.HightlightColor);

      this.loader.show("Pobieranie informacji o kierowcy...");
      this.driverPopupInfoService.fetchInfo(newMarker.id);

      this.markers.filter(m => m.id != newMarker.id).forEach(m => {
        m.setIcon(Truck.Normal);
        m.currentColor = Truck.NormalColor;
        m.setPathColor(Truck.NormalColor);
      });

      this.clickedMarker = newMarker;
    });

    newMarker.marker.addTo(this.map);
    this.markers.push(newMarker);
  };

  private removeMarker = (id: string): void => {
    let marker = this.markers.find(x => x.id === id);

    if (marker) {
      let index = this.markers.findIndex(x => x.id === id);
      this.markers.splice(index, 1);

      marker.marker.removeFrom(this.map);
      marker.path.forEach(path => path.removeFrom(this.map));
    }
  }

  private drawPath(marker: TruckrLeafletMapMarker, location1: Coordinates, location2: Coordinates): void {
    let latlngs = [
      L.latLng([location1.latitude, location1.longitude]),
      L.latLng([location2.latitude, location2.longitude])
    ];
    let polyline = L.polyline(latlngs, { color: marker.currentColor }).addTo(this.map);

    marker.path.push(polyline);

    marker.slicePath(this.map.getZoom()).forEach(line => line.removeFrom(this.map));
  }

  private openModal = () => {
    this.driverInfoModalRef = this.modalService.show(this.driverInfoModal);
  }

  closeModal = () => {
    this.clickedMarker.setIcon(Truck.Normal);
    this.clickedMarker.currentColor = Truck.NormalColor;
    this.clickedMarker.setPathColor(Truck.NormalColor);
  }
}