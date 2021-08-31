import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateAssignmentModel } from './create-assignment-model';
import { CreateAssignmentResult } from './create-assignment-result';
import { Observable } from 'rxjs';
import { Helper } from 'src/infrastructure/common/helper';

@Injectable({
  providedIn: 'root'
})
export class CreateAssignmentService {

  constructor(@Inject("TRANSPORT_SERVICE_URL") private readonly transportServiceUrl: string, 
  private readonly http: HttpClient) { }

  public create = (model:CreateAssignmentModel):Observable<CreateAssignmentResult> => {
    return this.http.post<CreateAssignmentResult>(`${this.transportServiceUrl}/assignments`, Helper.toFormData(model));
  }
}
