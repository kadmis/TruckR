import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AssignmentDetailsResult } from './assignment-details-result';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AssignmentDetailsService {

  constructor(@Inject("TRANSPORT_SERVICE_URL") private readonly transportServiceUrl: string, 
  private readonly http: HttpClient) { }

  fetch = (id:string):Observable<AssignmentDetailsResult> => {
    return this.http.get<AssignmentDetailsResult>(`${this.transportServiceUrl}/assignments/${id}`);
  }
}
