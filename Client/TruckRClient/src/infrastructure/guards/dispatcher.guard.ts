import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserManagerService } from '../auth/user-manager.service';

@Injectable({
  providedIn: 'root'
})
export class DispatcherGuard implements CanActivate {
  constructor(
    private roleManager: UserManagerService, 
    private router: Router){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let isDispatcher = this.roleManager.isDispatcher;

    if(!isDispatcher)
      this.router.navigate(["/home"]);

    return isDispatcher;
  }
  
}
