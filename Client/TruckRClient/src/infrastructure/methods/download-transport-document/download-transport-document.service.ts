import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DownloadTransportDocumentService {

  constructor(@Inject("TRANSPORT_SERVICE_URL") private readonly transportServiceUrl: string, 
  private readonly http: HttpClient) { }

  public download = (assignmentId: string): Observable<Blob> => {
    return this.http.get(`${this.transportServiceUrl}/assignments/${assignmentId}/download-document`, {responseType:'blob'});
  };
}
