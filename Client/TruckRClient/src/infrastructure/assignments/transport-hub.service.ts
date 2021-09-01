import { Injectable, Inject } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import * as SignalR from "@microsoft/signalr";
import { IHttpConnectionOptions } from '@microsoft/signalr';
import { TokenManagerService } from '../auth/token-manager.service';
import { NotificationService } from '../common/notification.service';
import { UserManagerService } from '../auth/user-manager.service';

@Injectable({
  providedIn: 'root'
})
export class TransportHubService {

  private assignmentCreatedSubject: Subject<AssignmentCreated>;
  assignmentCreated$: Observable<AssignmentCreated>;

  private assignmentTakenSubject: Subject<AssignmentTaken>;
  assignmentTaken$: Observable<AssignmentTaken>;

  private assignmentFinishedSubject: Subject<AssignmentFinished>;
  assignmentFinished$: Observable<AssignmentFinished>;

  private assignmentFailedSubject: Subject<AssignmentFailed>;
  assignmentFailed$: Observable<AssignmentFailed>;

  private assignmentExpiredSubject: Subject<AssignmentExpired>;
  assignmentExpired$: Observable<AssignmentExpired>;

  private connectedSubject: Subject<boolean>;
  $connected: Observable<boolean>;

  private hubConnection: SignalR.HubConnection;

  constructor(
    private tokenManager: TokenManagerService,
    private notifications: NotificationService,
    private userManager: UserManagerService,
    @Inject("TRANSPORT_HUB_URL") private readonly transportHuburl: string) {
      this.assignmentCreatedSubject = new Subject<AssignmentCreated>();
      this.assignmentCreated$ = this.assignmentCreatedSubject.asObservable();
  
      this.assignmentTakenSubject = new Subject<AssignmentTaken>();
      this.assignmentTaken$ = this.assignmentTakenSubject.asObservable();
  
      this.assignmentFinishedSubject = new Subject<AssignmentFinished>();
      this.assignmentFinished$ = this.assignmentFinishedSubject.asObservable();

      this.connectedSubject = new Subject<boolean>();
      this.$connected = this.connectedSubject.asObservable();

      this.assignmentFailedSubject = new Subject<AssignmentFailed>();
      this.assignmentFailed$ = this.assignmentFailedSubject.asObservable();
  
      this.assignmentExpiredSubject = new Subject<AssignmentExpired>();
      this.assignmentExpired$ = this.assignmentExpiredSubject.asObservable();

      const options: IHttpConnectionOptions = {
        accessTokenFactory: () => {
          return this.tokenManager.apiToken;
        }
      };
  
      this.hubConnection = new SignalR.HubConnectionBuilder()
      .withAutomaticReconnect()
      .configureLogging(SignalR.LogLevel.Debug)
      .withUrl(this.transportHuburl, options)
      .build();
  };

  public connect = (): void => {
    if(this.hubConnection) {
      this.hubConnection
      .start()
      .then(() => {
        this.connectUser(this.userManager.userId, this.userManager.role)
        this.notifications.showSuccess('Połączono z serwisem synchronizacji zleceń!');
        this.connectedSubject.next(true);
      })
      .catch((e)=> {
        this.notifications.showError('Nie udało się połączyć z serwisem synchronizacji zleceń.');
      });
    }
  };

  public disconnect = (showMessage?:boolean):void => {
    if(this.isConnected) {
      this.hubConnection
      .stop()
      .then(()=>{
        this.disconnectUser(this.userManager.userId, this.userManager.role);
        this.connectedSubject.next(false);
      })
      .catch((err)=>this.notifications.showWarning('Wystąpił problem przy rozłączaniu z serwisem synchronizacji zleceń. '))
      .finally(()=> {
        if(showMessage) {
          this.notifications.showInfo('Rozłączono z serwisem synchronizacji zleceń.');
        }
      });
    } else {
      this.connectedSubject.next(false);
    }
  };

  public connectUser = (userId: string, role:string):void => {
    this.hubConnection
    .invoke('AddUserConnection', userId, role)
    .catch(()=>this.notifications.showWarning('Błąd połączenia z serwisem synchronizacji zleceń.'))
    .finally();
  }

  public disconnectUser = (userId: string, role:string):void => {
    this.hubConnection
    .invoke('RemoveUserConnection', userId, role)
    .catch(()=>this.notifications.showWarning('Błąd połączenia z serwisem synchronizacji zleceń.'))
    .finally();
  }

  public get isConnected():boolean {
    return this.hubConnection && this.hubConnection.state === SignalR.HubConnectionState.Connected;
  }
  public get isDisconnected():boolean {
    return !this.hubConnection || this.hubConnection && this.hubConnection.state === SignalR.HubConnectionState.Disconnected;
  }
  public get isDisconnecting():boolean {
    return !this.hubConnection || this.hubConnection && this.hubConnection.state === SignalR.HubConnectionState.Disconnecting;
  }

  public assignmentCreated = (assignmentId:string, title:string):void => {
    this.hubConnection
    .invoke('CreateAssignment', new AssignmentCreated(assignmentId, title))
    .catch(()=>this.notifications.showWarning('Brak połączenia z serwisem synchronizacji zleceń.'))
    .finally();
  };
  public onAssignmentCreated = ():void => {
    this.hubConnection.on('AssignmentCreated', (model:AssignmentCreated)=> {
      this.assignmentCreatedSubject.next(model);
    });
  };

  public assignmentTaken = (assignmentId:string, title:string):void => {
    this.hubConnection
    .invoke('TakeAssignment', new AssignmentTaken(assignmentId, title))
    .catch((err)=>this.notifications.showWarning('Błąd połączenia z serwisem synchronizacji zleceń.'))
    .finally();
  };
  public onAssignmentTaken = ():void => {
    this.hubConnection.on('AssignmentTaken', (model:AssignmentTaken)=> {
      this.assignmentTakenSubject.next(model);
    });
  };

  public assignmentFinished = (assignmentId:string, title:string):void => {
    this.hubConnection
    .invoke('FinishAssignment', new AssignmentFinished(assignmentId, title))
    .catch((err)=>this.notifications.showWarning('Błąd połączenia z serwisem synchronizacji zleceń.'))
    .finally();
  };
  public onAssignmentFinished = ():void => {
    this.hubConnection.on('AssignmentFinished', (model:AssignmentFinished)=> {
      this.assignmentFinishedSubject.next(model);
    });
  };

  public onAssignmentFailed = ():void => {
    this.hubConnection.on('AssignmentFailed', (model:AssignmentFailed)=> {
      this.assignmentFailedSubject.next(model);
    });
  };

  public onAssignmentExpired = ():void => {
    this.hubConnection.on('AssignmentExpired', (model:any)=> {
      console.log(model);
      this.assignmentExpiredSubject.next(model);
    });
  };
}

export class AssignmentCreated {
  constructor(public assignmentId:string, public title:string){}
}

export class AssignmentTaken {
  constructor(public assignmentId:string, public title:string){}
}

export class AssignmentFinished {
  constructor(public assignmentId:string, public title:string){}
}

export class AssignmentFailed {
  constructor(public assignmentId:string){}
}

export class AssignmentExpired {
  constructor(public assignmentId:string){}
}
