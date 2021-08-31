import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DriversAssignmentResult } from './drivers-assignment-result';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DriversAssignmentService {

  constructor(@Inject("TRANSPORT_SERVICE_URL") private readonly transportServiceUrl: string, 
  private readonly http: HttpClient) { }

  public fetch = (driverId: string):Observable<DriversAssignmentResult> => {
    return this.http.get<DriversAssignmentResult>(`${this.transportServiceUrl}/assignments/drivers-current-assignment/${driverId}`)
  }
}
