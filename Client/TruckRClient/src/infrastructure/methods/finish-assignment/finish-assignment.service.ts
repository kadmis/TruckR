import { Injectable, Inject } from '@angular/core';
import { FinishAssignmentResult } from './finish-assignment-result';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FinishAssignmentService {

  constructor(@Inject("TRANSPORT_SERVICE_URL") private readonly transportServiceUrl: string, 
  private readonly http: HttpClient) { }

  public finish = (id:string):Observable<FinishAssignmentResult> => {
    return this.http.put<FinishAssignmentResult>(`${this.transportServiceUrl}/assignments/${id}/finish`,{});
  }
}
