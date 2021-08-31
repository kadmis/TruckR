import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { FreeAssignmentsModel } from './free-assignments-model';
import { FreeAssignmentsResult } from './free-assignments-result';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FreeAssignmentsService {

  constructor(@Inject("TRANSPORT_SERVICE_URL") private readonly transportServiceUrl: string, 
  private readonly http: HttpClient) { }

  public fetch = (model:FreeAssignmentsModel):Observable<FreeAssignmentsResult> => {
    let params = new HttpParams();

    params = params.append("pageSize", model.pageSize.toString());
    params = params.append("page", model.page.toString());

    return this.http.get<FreeAssignmentsResult>(`${this.transportServiceUrl}/assignments/free-assignments`,{params});
  }
}
