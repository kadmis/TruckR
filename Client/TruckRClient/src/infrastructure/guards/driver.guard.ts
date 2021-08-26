import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserManagerService } from '../auth/user-manager.service';

@Injectable({
  providedIn: 'root'
})
export class DriverGuard implements CanActivate {
  constructor(
    private roleManager: UserManagerService, 
    private router: Router){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      let isDriver = this.roleManager.isDriver;

      if(!isDriver)
        this.router.navigate(["/home"]);
  
      return isDriver;
  }
  
}
