import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthenticateResult } from './authenticate-result';
import { AuthenticateModel } from './authenticate-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticateService {

  constructor(@Inject("AUTH_SERVICE_URL") private readonly authServiceUrl: string, 
  private readonly http: HttpClient) { }

  public authenticate = (model: AuthenticateModel):Observable<AuthenticateResult> => {
    return this.http.post<AuthenticateResult>(`${this.authServiceUrl}/auth/authenticate`, model);
  }
}
