import { Injectable } from '@angular/core';
import { LoginManagerService } from './login-manager.service';
import { UserDetailsService } from '../methods/user-details/user-details.service';

@Injectable({
  providedIn: 'root'
})
export class UserManagerService {

  private roleKey: string = "Role";
  private idKey: string = "Id";

  private dispatcher: string = "Dispatcher";
  private driver: string = "Driver";

  private _fullName: string;

  constructor(private fetchDetails: UserDetailsService) {
  }

  public setRole = (role:string) => {
    localStorage.setItem(this.roleKey, role);
  }

  public setId = (id:string) => {
    localStorage.setItem(this.idKey, id);
  }

  public clear = ():void => {
    localStorage.removeItem(this.roleKey);
    localStorage.removeItem(this.idKey);
    this._fullName = "";
  }

  public fetchUserDetails = ():void => {
    this.fetchDetails.fetch().subscribe((res)=>{
      if(res.successful) {
        this._fullName = res.firstName + ' ' + res.lastName;
      }
    })
  }

  public get fullName():string {
    return this._fullName;
  }

  public get role():string {
    switch(this.currentRole) {
      case this.dispatcher:
        return 'Dyspozytor';
      case this.driver:
        return 'Kierowca';
      default:
        return '';
    }
  }

  public get userId():string {
    return localStorage.getItem(this.idKey);
  }

  public get isDispatcher():boolean {
    return this.currentRole && this.currentRole === this.dispatcher;
  }

  public get isDriver():boolean {
    return this.currentRole && this.currentRole === this.driver;
  }

  private get currentRole():string | null {
    return localStorage.getItem(this.roleKey);
  }
}
