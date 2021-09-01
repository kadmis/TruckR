import { Injectable } from '@angular/core';
import { LoginManagerService } from './login-manager.service';
import { UserDetailsService } from '../methods/user-details/user-details.service';
import { TokenManagerService } from './token-manager.service';
import { getTokenData } from './token-decoder';

@Injectable({
  providedIn: 'root'
})
export class UserManagerService {
  private dispatcher: string = "Dispatcher";
  private driver: string = "Driver";

  private _fullName: string;
  private _id: string;
  private _role: string;
  private _authenticationId: string;

  constructor(private tokenManager: TokenManagerService) {
    this.refreshUserData();
  }

  public refreshUserData = ():void => {
    let userData = getTokenData(this.tokenManager.apiToken);

    this._role = userData.role;
    this._id = userData.userId;
    this._fullName = userData.fullName;
    this._authenticationId = userData.authenticationId;
  }

  public clear = ():void => {
    this._fullName = "";
    this._id = "";
    this._role = "";
  }

  public get roleName():string {
    switch(this._role) {
      case this.dispatcher:
        return 'Dyspozytor';
      case this.driver:
        return 'Kierowca';
      default:
        return '';
    }
  }

  public get fullName():string {
    return this._fullName;
  }

  public get userId():string {
    return this._id;
  }

  public get authenticationId():string {
    return this._authenticationId;
  }

  public get role():string {
    return this._role;
  }

  public get isDispatcher():boolean {
    return this._role && this._role === this.dispatcher;
  }

  public get isDriver():boolean {
    return this._role && this._role === this.driver;
  }
}
