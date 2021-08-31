import { Component, OnInit, OnDestroy } from '@angular/core';
import { UserManagerService } from 'src/infrastructure/auth/user-manager.service';
import { RefreshStateManagerService } from 'src/infrastructure/auth/refresh-state-manager.service';
import { LoginManagerService } from 'src/infrastructure/auth/login-manager.service';
import { NotificationService } from 'src/infrastructure/common/notification.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { TokenManagerService } from 'src/infrastructure/auth/token-manager.service';
import { LogoutPipelineService } from 'src/infrastructure/common/logout-pipeline.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {
  isCollapsed = true;
  private refreshStateSubscription: Subscription;
  private logoutSubscription: Subscription;
  constructor(
    private userManager: UserManagerService,
    private refreshState: RefreshStateManagerService,
    private loginManager: LoginManagerService,
    private notifications: NotificationService,
    private tokenManager: TokenManagerService,
    private logoutPipelineService: LogoutPipelineService,
    private router: Router) {
      
      if(this.loggedIn) {       
        let instantRefresh = this.tokenManager.tokenData.msToExpire < this.tokenManager.refreshInterval;
        this.refreshState.set(instantRefresh);
      }

      this.refreshStateSubscription = 
      this.refreshState.refresh$.subscribe(refreshed=> {
        if(refreshed) {
          this.notifications.showInfo('Odświeżono sesję.');
        } else {
          this.notifications.showWarning('Próba odświeżenia sesji nie powiodła się.');
          this.logout();
        }
      });

      this.logoutSubscription = 
      this.loginManager.loggedOut$.subscribe(loggedState=>{
        if(loggedState.showMessage===true)
          this.notifications.showInfo('Wylogowano.');

        this.router.navigate(['']);
      });
    }

  ngOnInit(): void {
  }

  ngOnDestroy():void {
    this.refreshStateSubscription.unsubscribe();
    this.logoutSubscription.unsubscribe();
  }

  logout = ():void => {
    this.logoutPipelineService.logout();
  }

  goToProfile = ():void => {

  }

  get fullname():string {
    return this.userManager.fullName;
  }

  get loggedIn():boolean {
    return this.loginManager.loggedIn;
  }
}
