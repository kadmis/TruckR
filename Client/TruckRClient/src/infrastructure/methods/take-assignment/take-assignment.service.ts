import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TakeAssignmentResult } from './take-assignment-result';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TakeAssignmentService {

  constructor(@Inject("TRANSPORT_SERVICE_URL") private readonly transportServiceUrl: string, 
  private readonly http: HttpClient) { }

  public take = (id:string):Observable<TakeAssignmentResult> => {
    return this.http.put<TakeAssignmentResult>(`${this.transportServiceUrl}/assignments/${id}/take`,{});
  }
}
