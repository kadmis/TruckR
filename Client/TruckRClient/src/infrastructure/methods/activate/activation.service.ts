import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivationResult } from './activation-result';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ActivationService {

  constructor(@Inject("AUTH_SERVICE_URL") private readonly authServiceUrl: string, 
  private readonly http: HttpClient) { }

  public activate = (userId: string, activationId: string):Observable<ActivationResult> => {
    return this.http.patch<ActivationResult>(`${this.authServiceUrl}/users/activate/${userId}/${activationId}`,{});
  }
}
