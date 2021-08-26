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
  private loggedInSubject: Subject<LoggedInState>;
  loggedIn$: Observable<LoggedInState>;

  private loggedOutSubject: Subject<boolean>;
  loggedOut$: Observable<boolean>;

  constructor(
    private refreshState: RefreshStateManagerService,
    private tokenManager: TokenManagerService,
    private authenticateService: AuthenticateService,
    private userManager: UserManagerService) {
      this.loggedInSubject = new Subject<LoggedInState>();
      this.loggedIn$ = this.loggedInSubject.asObservable();

      this.loggedOutSubject = new Subject<boolean>();
      this.loggedOut$ = this.loggedOutSubject.asObservable();
    }
    
  logout = ():void => {
    this.refreshState.clear();
    this.tokenManager.clear();
    this.userManager.clear();
    this.loggedOutSubject.next(true);
  }

  login = (username: string, password: string):void => {
    let model = new AuthenticateModel(username, password);

    this.authenticateService.authenticate(model).subscribe((res)=>{
      if(res.successful) {
        this.tokenManager.setApiToken(res.token);
        this.tokenManager.setRefreshToken(res.refreshToken, res.refreshInterval);
        this.refreshState.set();
        this.userManager.setRole(res.role);
        this.userManager.setId(res.userId);
        this.userManager.fetchUserDetails();
      }
      this.loggedInSubject.next(new LoggedInState(res.successful, res.message));
    }, (err)=>this.loggedInSubject.next(new LoggedInState(false, err.message)));
  }

  get loggedIn():boolean {
    return this.tokenManager.apiToken !==null && this.tokenManager.refreshToken !== null;
  }
}

export class LoggedInState {
  constructor(public successful: boolean, public message: string){}
}
