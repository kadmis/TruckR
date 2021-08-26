import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { UserDetailsResult } from './user-details-result';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {

  constructor(
    @Inject("AUTH_SERVICE_URL") private readonly authServiceUrl: string, 
    private readonly http: HttpClient) { }

    public fetch = (userId?: string | null):Observable<UserDetailsResult> => {
      let params = new HttpParams();

      if(userId) {
        params = params.append('userId', userId);
      }
      
      return this.http.get<UserDetailsResult>(`${this.authServiceUrl}/users/details`, {params});
    }
}
