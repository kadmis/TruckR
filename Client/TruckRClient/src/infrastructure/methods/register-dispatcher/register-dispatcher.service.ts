import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RegisterDispatcherModel } from './register-dispatcher-model';
import { RegisterDispatcherResult } from './register-dispatcher-result';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegisterDispatcherService {

  constructor(
    @Inject("AUTH_SERVICE_URL") private readonly authServiceUrl: string, 
    private readonly http: HttpClient) { }

    register = (model: RegisterDispatcherModel):Observable<RegisterDispatcherResult> => {
      return this.http.post<RegisterDispatcherResult>(`${this.authServiceUrl}/auth/register-dispatcher`, model);
    }
}
