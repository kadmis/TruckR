import { Component, OnInit, ViewChild, TemplateRef, OnDestroy, ViewEncapsulation } from '@angular/core';
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { Subscription } from 'rxjs';
import { CreateAssignmentComponent } from '../../create-assignment/create-assignment.component';
import { AssignmentEventsService } from 'src/infrastructure/assignments/assignment-events.service';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class SideBarComponent implements OnInit, OnDestroy {

  private assignmentCreatedSubscription: Subscription;

  constructor(
    private modalService: BsModalService,
    private assignmentEvents: AssignmentEventsService) {
      this.assignmentCreatedSubscription =
      this.assignmentEvents.assignmentCreated$.subscribe(()=> {
        if(this.createAssignmentModalRef) {
          this.modalService.hide(this.createAssignmentModalRef.id);
        }
      })
  }

  ngOnDestroy(): void {
    this.assignmentCreatedSubscription.unsubscribe();
  }

  createAssignmentModalRef?: BsModalRef;
  @ViewChild('createAssignmentModal') createAssignmentModal: TemplateRef<CreateAssignmentComponent>;

  ngOnInit(): void {
  }

  public openCreateAssignmentModal = ():void => {
    this.createAssignmentModalRef = this.modalService.show(this.createAssignmentModal, {
      class: 'my-modal',
      backdrop: 'static'
    });
  };

  public closeCreateAssignmentModal = ():void => {
    if(this.createAssignmentModalRef)
      this.createAssignmentModalRef.hide();
  }
}
