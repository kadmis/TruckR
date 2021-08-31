import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TransportDocumentInfoResult } from './transport-document-info-result';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransportDocumentInfoService {

  constructor(@Inject("TRANSPORT_SERVICE_URL") private readonly transportServiceUrl: string, 
  private readonly http: HttpClient) { }

  fetch = (assignmentId: string):Observable<TransportDocumentInfoResult> => {
    return this.http.get<TransportDocumentInfoResult>(`${this.transportServiceUrl}/assignments/${assignmentId}/document-info`);
  }
}
