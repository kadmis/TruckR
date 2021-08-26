import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RefreshTokenModel } from './refresh-token-model';
import { RefreshTokenResult } from './refresh-token-result';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RefreshTokenService {

  constructor(@Inject("AUTH_SERVICE_URL") private readonly authServiceUrl: string, 
  private readonly http: HttpClient) { }

  public refresh = (model: RefreshTokenModel):Observable<RefreshTokenResult> => {
    return this.http.post<RefreshTokenResult>(`${this.authServiceUrl}/auth/refresh-token`, model);
  }
}
