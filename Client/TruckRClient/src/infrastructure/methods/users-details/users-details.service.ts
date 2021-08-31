import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UsersDetailsResult } from './users-details-result';

@Injectable({
  providedIn: 'root'
})
export class UsersDetailsService {

  constructor(
    @Inject("AUTH_SERVICE_URL") private readonly authServiceUrl: string, 
    private readonly http: HttpClient) { }

    public fetch = (userId?: string | null):Observable<UsersDetailsResult> => {
      let params = new HttpParams();

      if(userId) {
        params = params.append('userId', userId);
      }
      
      return this.http.get<UsersDetailsResult>(`${this.authServiceUrl}/users/details`, {params});
    }
}
