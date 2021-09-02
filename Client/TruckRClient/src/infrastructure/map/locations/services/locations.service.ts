import { Injectable, NgZone, Inject } from '@angular/core';
import { UserLocation } from '../models/user-location';
import { Subject, Observable, Subscription } from 'rxjs';
import * as SignalR from "@microsoft/signalr";
import { TokenManagerService } from 'src/infrastructure/auth/token-manager.service';
import { IHttpConnectionOptions } from '@microsoft/signalr';
import { NotificationService } from 'src/infrastructure/common/notification.service';
import { UserManagerService } from 'src/infrastructure/auth/user-manager.service';
import { LocationSpooferService } from './location-spoofer.service';
import { Coordinates } from '../models/coordinates';

@Injectable({
  providedIn: 'root'
})
export class LocationsService {

  private userDisconnectedSubject: Subject<string>;
  $userDisconnected: Observable<string>;

  private connectedSubject: Subject<ConnectedState>;
  $connected: Observable<ConnectedState>;

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

    this.connectedSubject = new Subject<ConnectedState>();
    this.$connected = this.connectedSubject.asObservable();

    this.userDisconnectedSubject = new Subject<string>();
    this.$userDisconnected = this.userDisconnectedSubject.asObservable();

    const options: IHttpConnectionOptions = {
      accessTokenFactory: () => {
        return this.tokenManager.apiToken;
      }
    };

    this.hubConnection = new SignalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .configureLogging(SignalR.LogLevel.Debug)
    .withUrl(this.locationsHubUrl, options)
    .build();
  };

  public connect = (): void => {
    if(this.hubConnection) {
      this.hubConnection
      .start()
      .then(() => {
        this.notifications.showSuccess('Połączono z serwisem geolokalizacji!');
        this.hubConnection.onreconnecting(()=>this.notifications.showInfo('Wznawianie połączenia z serwisem geolokalizacji...'));
        this.hubConnection.onreconnected(()=>this.notifications.showSuccess('Wznowiono połączenie z serwisem geolokalizacji.'));
        this.connectedSubject.next(new ConnectedState(true));
      })
      .catch((e)=> {
        this.notifications.showError('Nie udało się połączyć z serwisem geolokalizacji.');
      });
    }
  };

  public disconnect = (showMessage?:boolean):void => {
    if(this.isConnected) {
      this.hubConnection
      .stop()
      .then(()=>{
        this.connectedSubject.next(new ConnectedState(false));
      })
      .catch((err)=>this.notifications.showWarning('Wystąpił problem przy rozłączaniu z serwisem geolokalizacji. '+err.message))
      .finally(()=> {
        if(showMessage) {
          this.notifications.showInfo('Rozłączono z serwisem geolokalizacji.');
        }
      });
    } else {
      this.connectedSubject.next(new ConnectedState(false));
    }
  };

  public onLocationReceived = ():void => {
    this.hubConnection.on('LocationShared', (data:UserLocation)=>{
      if(data) {
        this.locationReceivedSubject.next(data);
      }
    });
  };

  public onUserDisconnected = ():void => {
    this.hubConnection.on('UserDisconnected', (userId:string)=> {
      if(userId) {
        this.userDisconnectedSubject.next(userId);
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
    }, () => this.notifications.showError(
      "Brak dostępu do lokalizacji. Aby zapewnić jej udostępnianie w systemie, należy zezwolić przeglądarce na jej używanie."), {
    enableHighAccuracy: false,
    timeout: 27000,
    maximumAge: 30000 });
  };

  public startWatchingSpoofedLocation = (): void => {

    let newPath = [
      new Coordinates(50.39109398399044, 18.945544197567035),
      new Coordinates(50.39104596379056, 18.945964254731386),
      new Coordinates(50.39095361711569, 18.946844926303548),
      new Coordinates(50.39087419882983, 18.947577853691104),
      new Coordinates(50.39080770900074, 18.94839479247541),
      new Coordinates(50.3907227496387, 18.949052399232876),
      new Coordinates(50.390630402332654, 18.949805605206613),
      new Coordinates(50.39049926884927, 18.95065441039696),
      new Coordinates(50.39036259412785, 18.95162488734993),
      new Coordinates(50.390244388648384, 18.952366505517574),
      new Coordinates(50.39008924350964, 18.953470242001238),
      new Coordinates(50.39001167074256, 18.95433063499631),
      new Coordinates(50.39000982377052, 18.955405401947857),
      new Coordinates(50.39007446774854, 18.955532867570145),
      new Coordinates(50.3905860755639, 18.955602394305007),
      new Coordinates(50.39121772813286, 18.955651642384648),
      new Coordinates(50.3917791900196, 18.95506935623547),
      new Coordinates(50.39191482191523, 18.954857825109645),
      new Coordinates(50.391817687016506, 18.954658584467307),
      new Coordinates(50.39166468188961, 18.954359604456027)
    ];

    this.locationSpoofer.provideSpoofedLocations(newPath);
    this.locationSpoofer.useFakeGeolocator();
    this.startWatchingLocation();
    this.locationSpoofer.startSendingFakeGeoLocations();
  };

  public stopWatchingLocation = ():void => {
    navigator.geolocation.clearWatch(this.watchId);
    this.notifications.showInfo("Zatrzymano udostępnianie lokalizacji.");
  };

  public stopWatchingSpoofedLocation = ():void => {
    this.locationSpoofer.restoreDefaultGeolocator();
    this.locationSpoofer.stopSendingFakeGeoLocations();
  };

  public get isConnected():boolean {
    return this.hubConnection && this.hubConnection.state === SignalR.HubConnectionState.Connected;
  }
  public get isDisconnected():boolean {
    return !this.hubConnection || this.hubConnection && this.hubConnection.state === SignalR.HubConnectionState.Disconnected;
  }
  public get isDisconnecting():boolean {
    return !this.hubConnection || this.hubConnection && this.hubConnection.state === SignalR.HubConnectionState.Disconnecting;
  }

  private shareIfPositionChanged = (
    latitude: number, 
    longitude: number): void => {
    if(latitude != this.currentLatitude || longitude != this.currentLongitude) {

      this.currentLongitude = longitude;
      this.currentLatitude = latitude; 

      this.hubConnection
        .invoke('ShareLocation', new UserLocation(
          this.userManager.userId, 
          this.currentLongitude, 
          this.currentLatitude))
        .catch(()=>this.notifications
        .showWarning('Brak połączenia z serwisem geolokalizacji.'))
        .finally();
    }
  };
}

export class ConnectedState {
  constructor(public connected: boolean){}
}
