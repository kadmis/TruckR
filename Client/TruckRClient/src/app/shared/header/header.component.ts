import { Component, OnInit, OnDestroy } from '@angular/core';
import { UserManagerService } from 'src/infrastructure/auth/user-manager.service';
import { RefreshStateManagerService } from 'src/infrastructure/auth/refresh-state-manager.service';
import { LoginManagerService } from 'src/infrastructure/auth/login-manager.service';
import { NotificationService } from 'src/infrastructure/common/notification.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import jwt_decode from "jwt-decode";
import { TokenManagerService } from 'src/infrastructure/auth/token-manager.service';

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
    private tokenManager: TokenManagerService) {
      
      if(this.loggedIn) {        
        this.notifications.showInfo('Ustanowiono odświeżanie sesji.');
        let instantRefresh = this.tokenManager.tokenData.msToExpire < this.tokenManager.refreshInterval;
        this.refreshState.set(instantRefresh);
      }

      this.refreshStateSubscription = 
      this.refreshState.refresh$.subscribe(refreshed=>{
        if(refreshed) {
          this.notifications.showInfo('Odświeżono sesję.');
        } else {
          this.notifications.showWarning('Próba odświeżenia sesji nie powiodła się.');
        }
      });

      this.logoutSubscription = 
      this.loginManager.loggedOut$.subscribe(loggedOut=>{
        if(loggedOut)
          this.notifications.showInfo('Wylogowano.');
      });
    }

  ngOnInit(): void {
  }

  ngOnDestroy():void {
    this.refreshStateSubscription.unsubscribe();
    this.logoutSubscription.unsubscribe();
  }

  logout = ():void => {
    this.loginManager.logout();
  }

  get fullname():string {
    return this.userManager.fullName;
  }

  get loggedIn():boolean {
    return this.loginManager.loggedIn;
  }
}
