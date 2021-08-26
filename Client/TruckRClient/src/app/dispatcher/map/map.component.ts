import { Component, OnInit, ViewChild } from '@angular/core';
import { LocationsService } from 'src/infrastructure/map/locations/locations.service';
import { UserLocation } from 'src/infrastructure/map/locations/user-location';
import { Subscription } from 'rxjs';
import { NotificationService } from 'src/infrastructure/common/notification.service';
import { GoogleMap, MapInfoWindow, MapMarker } from '@angular/google-maps';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit {

  @ViewChild(GoogleMap, { static: false }) map: GoogleMap
  @ViewChild(MapInfoWindow, { static: false }) info: MapInfoWindow

  zoom = 12
  center: google.maps.LatLngLiteral
  options: google.maps.MapOptions = {
    zoomControl: false,
    scrollwheel: false,
    disableDoubleClickZoom: true,
    mapTypeId: 'hybrid',
    maxZoom: 15,
    minZoom: 8,
  }
  markers = []
  infoContent = ''

  locationSharedSubscription!: Subscription;
  public locations: UserLocation[];

  constructor(
    public locationsService: LocationsService,
    public notificationService: NotificationService) {
    this.locations = new Array<UserLocation>();
  }

  ngOnInit(): void {
    this.initConnection();
    navigator.geolocation.getCurrentPosition((position) => {
      this.center = {
        lat: position.coords.latitude,
        lng: position.coords.longitude,
      }
    });
  }

  ngOnDestroy(): void {
    if(this.locationSharedSubscription)
      this.locationSharedSubscription.unsubscribe();
    
    this.locationsService.disconnect();
  }

  zoomIn() {
    if (this.zoom < this.options.maxZoom) this.zoom++
  }

  zoomOut() {
    if (this.zoom > this.options.minZoom) this.zoom--
  }

  click(event: google.maps.MouseEvent) {
    console.log(event)
  }

  logCenter() {
    console.log(JSON.stringify(this.map.getCenter()))
  }

  addMarker() {
    this.markers.push({
      position: {
        lat: this.center.lat + ((Math.random() - 0.5) * 2) / 10,
        lng: this.center.lng + ((Math.random() - 0.5) * 2) / 10,
      },
      label: {
        color: 'red',
        text: 'Marker label ' + (this.markers.length + 1),
      },
      title: 'Marker title ' + (this.markers.length + 1),
      info: 'Marker info ' + (this.markers.length + 1),
      options: {
        animation: google.maps.Animation.BOUNCE,
      },
    })
  }

  openInfo(marker: MapMarker, content) {
    this.infoContent = content
    this.info.open(marker)
  }

  private initConnection = ():void => {
    this.locationsService.connect();
    this.locationsService.$connected.subscribe(res=>{
      if(res) {
        this.locationsService.onLocationReceived();
        this.subscribeToLocationReceived();
      }
    });
  }

  private subscribeToLocationReceived = ():void => {
    this.locationSharedSubscription = this.locationsService.$locationReceived.subscribe(location=>{
      if(location) {
        this.notificationService.showInfo('Received location from ' + location.userId);
        this.addOrUpdateLocation(location);
      }
    });
  }

  private addOrUpdateLocation = (location:UserLocation):void => {
    let loc = this.locations.find(x=>x.userId==location.userId);

    if(loc) {
      let index = this.locations.indexOf(loc);
      this.locations.splice(index,1);
    }

    this.locations.push(location);
  }
}
