import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegisterDriverResult } from './register-driver-result';
import { RegisterDriverModel } from './register-driver-model';

@Injectable({
  providedIn: 'root'
})
export class RegisterDriverService {

  constructor(
    @Inject("AUTH_SERVICE_URL") private readonly authServiceUrl: string, 
    private readonly http: HttpClient) { }

    register = (model: RegisterDriverModel):Observable<RegisterDriverResult> => {
      return this.http.post<RegisterDriverResult>(`${this.authServiceUrl}/auth/register-driver`, model);
    }
}
