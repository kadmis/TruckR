import { Injectable } from '@angular/core';
import { UserDetailsService } from 'src/infrastructure/methods/user-details/user-details.service';
import { DriversAssignmentService } from 'src/infrastructure/methods/drivers-assignment/drivers-assignment.service';
import { forkJoin, Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DriverInfoMapPopupService {

  private infoRetrievedSubject: Subject<DriverPopupInfo>;
  $infoRetrieved: Observable<DriverPopupInfo>;

  constructor(
    private userDetails: UserDetailsService, 
    private driversAssignment: DriversAssignmentService) {
      this.infoRetrievedSubject = new Subject<DriverPopupInfo>();
      this.$infoRetrieved = this.infoRetrievedSubject.asObservable();
    }

  public fetchInfo = (driverId:string):void => {
    forkJoin(this.userDetails.fetch(driverId), this.driversAssignment.fetch(driverId)).subscribe(results=>{

      let driverInfo = new DriverPopupInfo();
      driverInfo.id = driverId;

      let driverResult = results[0];
      let assignmentResult = results[1];

      if(driverResult.successful) {
        driverInfo.fullname = driverResult.details.firstName + ' ' + driverResult.details.lastName;
        driverInfo.phone = driverResult.details.phone;
      }
      if(assignmentResult && assignmentResult.successful) {
        let data = assignmentResult.data;
        driverInfo.hasAnAssignment = true;
        driverInfo.assignmentTitle = data.title;
        driverInfo.assignmentStart = data.startingStreet + ', ' + data.startingPostalCode + ' ' + data.startingCity + ', ' + data.startingCountry;
        driverInfo.assignmentDestination = data.destinationStreet + ', ' + data.destinationPostalCode + ' ' + data.destinationCity + ', ' + data.destinationCountry;
        driverInfo.assignmentDeadline = data.deadline;
      } else {
        driverInfo.hasAnAssignment = false;
      }

      this.infoRetrievedSubject.next(driverInfo);
    });
  }
}

export class DriverPopupInfo {
  id:string;

  fullname:string;
  phone:string;

  hasAnAssignment: boolean;

  assignmentTitle: string;
  assignmentStart: string;
  assignmentDestination: string;
  assignmentDeadline:Date;
}
