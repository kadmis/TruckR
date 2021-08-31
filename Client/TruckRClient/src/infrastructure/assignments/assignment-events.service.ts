import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AssignmentEventsService {

  private assignmentCreatedSubject: Subject<string>;
  assignmentCreated$: Observable<string>;

  constructor() {  
    this.assignmentCreatedSubject = new Subject<string>();
    this.assignmentCreated$ = this.assignmentCreatedSubject.asObservable();
  }

  public assignmentCreated =(id:string)=> {
    this.assignmentCreatedSubject.next(id);
  }
}
