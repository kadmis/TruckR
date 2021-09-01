import { Injectable } from '@angular/core';
import { LoginManagerService } from '../auth/login-manager.service';
import { LocationsService } from '../map/locations/services/locations.service';
import { TransportHubService } from '../assignments/transport-hub.service';

@Injectable({
  providedIn: 'root'
})
export class LogoutPipelineService {

  public showLogoutMessage: boolean;
  
  constructor(
    private loginManager: LoginManagerService, 
    private locationsService: LocationsService,
    private transportHubService: TransportHubService) {

      this.locationsService.$connected.subscribe(connected=> {
        if(connected.connected === false) {
          this.transportHubService.disconnect(true);
        }         
      });

      this.transportHubService.$connected.subscribe(connected=> {
        if(connected===false) {
          this.loginManager.logout(this.showLogoutMessage);
        }
      });
  }

  public logout = (showLogoutMessage?:boolean):void => {
    this.showLogoutMessage = showLogoutMessage;
    this.locationsService.disconnect(showLogoutMessage);
  }
}
