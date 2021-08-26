import { Injectable, NgZone, Inject } from '@angular/core';
import { UserLocation } from './user-location';
import { Subject, Observable } from 'rxjs';
import * as SignalR from "@microsoft/signalr";
import { TokenManagerService } from 'src/infrastructure/auth/token-manager.service';
import { IHttpConnectionOptions } from '@microsoft/signalr';
import { NotificationService } from 'src/infrastructure/common/notification.service';
import { UserManagerService } from 'src/infrastructure/auth/user-manager.service';
import { LocationSpooferService } from './location-spoofer.service';

@Injectable({
  providedIn: 'root'
})
export class LocationsService {

  private connectedSubject: Subject<boolean>;
  $connected: Observable<boolean>;

  private locationReceivedSubject: Subject<UserLocation>;
  $locationReceived: Observable<UserLocation>;

  private hubConnection: SignalR.HubConnection;
  private watchId: number = 0;
  
  private currentLongitude: number = 0;
  private currentLatitude: number = 0;

  constructor(
    private zone: NgZone,
    private tokenManager: TokenManagerService,
    private notifications: NotificationService,
    private userManager: UserManagerService,
    private locationSpoofer: LocationSpooferService,
    @Inject("LOCATIONS_HUB_URL") private readonly locationsHubUrl: string) {

    this.locationReceivedSubject = new Subject<UserLocation>();
    this.$locationReceived = this.locationReceivedSubject.asObservable();

    this.connectedSubject = new Subject<boolean>();
    this.$connected = this.connectedSubject.asObservable();

    const options: IHttpConnectionOptions = {
      accessTokenFactory: () => {
        return this.tokenManager.apiToken;
      }
    };

    console.log('Establishing connection');
    this.hubConnection = new SignalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .configureLogging(SignalR.LogLevel.Information)
    .withUrl(this.locationsHubUrl, options)
    .build();
  };

  public connect = (): void => {
  this.hubConnection
    .start()
    .then(() => {
      this.notifications.showSuccess('Połączono z serwisem geolokalizacji!');
      this.connectedSubject.next(true);
    })
    .catch((e)=> { 
      this.notifications.showError('Nie udało się połączyć z serwisem geolokalizacji.');
      console.log(e.message);
    });
  };

  public disconnect = ():void => {
    if(this.hubConnection) {
      console.log('Closing connection');
      this.hubConnection
      .stop()
      .then(()=>{
        this.notifications.showInfo('Rozłączono z serwisem geolokalizacji.');
        this.connectedSubject.next(false);
      });
    } 
  };

  public onLocationReceived = ():void => {
    this.hubConnection.on('LocationShared', (data:UserLocation)=>{
      if(data) {
        console.log('Received location');
        this.locationReceivedSubject.next(data);
      }
    });
  };

  public startWatchingLocation = (): void => {
    this.watchId = navigator.geolocation.watchPosition(location => {
      if(location) {
          this.zone.run(() => {
            let longitude = location.coords.longitude;
            let latitude = location.coords.latitude;

            this.shareIfPositionChanged(latitude, longitude);
          });
      }
    }, error => {
      console.log(error.message);
    }, {
    enableHighAccuracy: false,
    timeout: 27000,
    maximumAge: 30000 });
  };

  public startWatchingSpoofedLocation = (): void => {
    this.locationSpoofer.useFakeGeolocator();
    this.startWatchingLocation();
    this.locationSpoofer.startSendingFakeGeoLocations();
  }

  private shareIfPositionChanged = (latitude: number, longitude: number): void => {
    if(latitude != this.currentLatitude || longitude != this.currentLongitude) {

      this.currentLongitude = longitude;
      this.currentLatitude = latitude; 

      this.hubConnection
        .invoke('ShareLocation', new UserLocation(this.userManager.userId, this.currentLongitude, this.currentLatitude))
        .catch(()=>this.notifications.showWarning('Brak połączenia z serwisem geolokalizacji.'));
    }
  };
}
