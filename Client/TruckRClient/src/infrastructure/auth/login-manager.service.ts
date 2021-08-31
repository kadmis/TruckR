import { Injectable } from '@angular/core';
import { TokenManagerService } from './token-manager.service';
import { Subject, Observable } from 'rxjs';
import { AuthenticateService } from '../methods/authenticate/authenticate.service';
import { AuthenticateModel } from '../methods/authenticate/authenticate-model';
import { RefreshStateManagerService } from './refresh-state-manager.service';
import { UserManagerService } from './user-manager.service';

@Injectable({
  providedIn: 'root'
})
export class LoginManagerService {
  private loggedInSubject: Subject<LoggedState>;
  loggedIn$: Observable<LoggedState>;

  private loggedOutSubject: Subject<LoggedState>;
  loggedOut$: Observable<LoggedState>;

  constructor(
    private refreshState: RefreshStateManagerService,
    private tokenManager: TokenManagerService,
    private authenticateService: AuthenticateService,
    private userManager: UserManagerService) {
      this.loggedInSubject = new Subject<LoggedState>();
      this.loggedIn$ = this.loggedInSubject.asObservable();

      this.loggedOutSubject = new Subject<LoggedState>();
      this.loggedOut$ = this.loggedOutSubject.asObservable();
    }
    
  logout = (showMessage?:boolean):void => {
    this.refreshState.clear();
    this.tokenManager.clear();
    this.userManager.clear();
    this.loggedOutSubject.next(new LoggedState(false, showMessage));
  };

  login = (username: string, password: string):void => {
    let model = new AuthenticateModel(username, password);

    this.authenticateService.authenticate(model).subscribe((res)=>{
      if(res.successful) {
        this.tokenManager.setApiToken(res.token);
        this.tokenManager.setRefreshToken(res.refreshToken, res.refreshInterval);
        this.refreshState.set();
        this.userManager.refreshUserData();
      }
      this.loggedInSubject.next(new LoggedState(res.successful, true));
    }, (err)=>this.loggedInSubject.next(new LoggedState(false, err.message)));
  };

  get loggedIn():boolean {
    return this.tokenManager.apiToken !==null && this.tokenManager.refreshToken !== null;
  };
}

export class LoggedState {
  constructor(public loggedIn: boolean, public showMessage: boolean = true){}
}
