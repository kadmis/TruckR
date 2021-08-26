import { Injectable } from '@angular/core';
import jwt_decode from "jwt-decode";
import { TokenDecoder, TokenData } from './token-decoder';

@Injectable({
  providedIn: 'root'
})
export class TokenManagerService {

  private apiTokenKey: string = "api-token";
  private refreshTokenKey: string = "refresh-token";
  private refreshIntervalKey: string = "refresh-interval";
  
  constructor() { }

  public setApiToken = (token: string):void => {
    localStorage.setItem(this.apiTokenKey, token);
  }

  public setRefreshToken = (token: string, refreshInterval: number):void => {
    localStorage.setItem(this.refreshTokenKey, token);
    localStorage.setItem(this.refreshIntervalKey, refreshInterval.toString())
  }

  public clear = ():void => {
    localStorage.removeItem(this.apiTokenKey);
    localStorage.removeItem(this.refreshTokenKey);
    localStorage.removeItem(this.refreshIntervalKey);
  }

  public get tokenData():TokenData {
    return TokenDecoder.getTokenData(this.apiToken);
  }

  public get tokenExpired():boolean {
    return TokenDecoder.getTokenData(this.apiToken).expired;
  }

  public get apiToken():string {
    return localStorage.getItem(this.apiTokenKey);
  }

  public get refreshToken():string {
    return localStorage.getItem(this.refreshTokenKey);
  }

  public get refreshInterval():number {
    return (Number)(localStorage.getItem(this.refreshIntervalKey));
  }
}
