import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { LocationsService } from 'src/infrastructure/map/locations/services/locations.service';
import { Subscription, forkJoin, Subject, Observable } from 'rxjs';
import { DriversAssignmentDTO } from 'src/infrastructure/methods/drivers-assignment/drivers-assignment-result';
import { DriversAssignmentService } from 'src/infrastructure/methods/drivers-assignment/drivers-assignment.service';
import { UserManagerService } from 'src/infrastructure/auth/user-manager.service';
import { FreeAssignmentDTO, FreeAssignmentsResult } from 'src/infrastructure/methods/free-assignments/free-assignments-result';
import { FreeAssignmentsService } from 'src/infrastructure/methods/free-assignments/free-assignments.service';
import { FreeAssignmentsModel } from 'src/infrastructure/methods/free-assignments/free-assignments-model';
import { NotificationService } from 'src/infrastructure/common/notification.service';
import { TakeAssignmentService } from 'src/infrastructure/methods/take-assignment/take-assignment.service';
import { LoaderService } from 'src/infrastructure/common/loader.service';
import { AssignmentDetailsService } from 'src/infrastructure/methods/assignment-details/assignment-details.service';
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { AssignmentDetailsDTO } from 'src/infrastructure/methods/assignment-details/assignment-details-result';
import { DownloadTransportDocumentService } from 'src/infrastructure/methods/download-transport-document/download-transport-document.service';
import { FinishAssignmentService } from 'src/infrastructure/methods/finish-assignment/finish-assignment.service';
import { TransportDocumentInfoService } from 'src/infrastructure/methods/transport-document-info/transport-document-info.service';
import * as FileSaver from 'file-saver';
import { TransportHubService } from 'src/infrastructure/assignments/transport-hub.service';

@Component({
  selector: 'app-driver-dashboard',
  templateUrl: './driver-dashboard.component.html',
  styleUrls: ['./driver-dashboard.component.scss']
})
export class DriverDashboardComponent implements OnInit {
  private locationsConnectionSubscription: Subscription;
  private transportHubConnectionSubscription: Subscription;
  private sharedConnectionSubscription: Subscription;
  private assignmentCreatedSubscription: Subscription;
  private assignmentTakenSubscription: Subscription;
  private assignmentFinishedSubscription: Subscription;
  private assignmentFailedSubscription: Subscription;
  private assignmentExpiredSubscription: Subscription;

  currentPage: number = 1;
  currentPageSize: number = 5;
  totalItems: number;

  currentAssignment: DriversAssignmentDTO;

  freeAssignmentsList: FreeAssignmentDTO[];
  assignmentDetails: AssignmentDetailsDTO;

  assignmentDetailsModalRef?: BsModalRef;
  @ViewChild('assignmentDetailsModal') assignmentDetailsModal: TemplateRef<any>;

  constructor(
    private locationsService: LocationsService,
    private transportHubService: TransportHubService,
    private userManager: UserManagerService,
    private currentDriversAssignment: DriversAssignmentService,
    private assignmentDetailsService: AssignmentDetailsService,
    private freeAssignments: FreeAssignmentsService,
    private takeAssignmentService: TakeAssignmentService,
    private finishAssignmentService: FinishAssignmentService,
    private notifications: NotificationService,
    private modalService: BsModalService,
    private transportDocumentFile: DownloadTransportDocumentService,
    private transportDocumentInfo: TransportDocumentInfoService,
    private loader: LoaderService) {

    this.locationsConnectionSubscription =
      this.locationsService.$connected.subscribe(connected => {
        if (connected.connected === false) {
          this.locationsService.stopWatchingSpoofedLocation();
          this.locationsService.stopWatchingLocation();

          this.locationsConnectionSubscription.unsubscribe();
        } else {
          this.locationsService.startWatchingSpoofedLocation();
        }
      });

    this.transportHubConnectionSubscription =
      this.transportHubService.$connected.subscribe(connected => {
        if (connected) {
          this.transportHubService.onAssignmentCreated();
          this.transportHubService.onAssignmentTaken();
          this.transportHubService.onAssignmentFinished();
          this.transportHubService.onAssignmentExpired();
          this.transportHubService.onAssignmentFailed();

          this.assignmentCreatedSubscription =
            this.transportHubService.assignmentCreated$.subscribe(res => {
              if (res) {
                this.refreshAssignmentsList();
              }
            });

          this.assignmentTakenSubscription =
            this.transportHubService.assignmentTaken$.subscribe(res => {
              if (res) {
                this.refreshAssignmentsList();
              }
            });

          this.assignmentFinishedSubscription =
            this.transportHubService.assignmentFinished$.subscribe(res => {
              if (res) {
                this.refreshAssignmentsList();
              }
            });

          this.assignmentExpiredSubscription =
            this.transportHubService.assignmentExpired$.subscribe(ass => {
              if (ass) {
                this.refreshAssignmentsList();
              }
            });

          this.assignmentFailedSubscription =
            this.transportHubService.assignmentFailed$.subscribe(ass => {
              if (ass && this.currentAssignment && ass.assignmentId === this.currentAssignment.id) {
                this.notifications.showError("Minął czas na wykonanie podjętego zlecenia!");
              }
            });
        }
      });
  }

  ngOnInit(): void {
    this.initConnection();
    this.fetchCurrentAssignment();
    this.fetchAssignments({ page: this.currentPage, itemsPerPage: this.currentPageSize });

    var body = document.getElementsByTagName("body")[0];
    body.classList.add("map-page");
  }

  ngOnDestroy(): void {
    var body = document.getElementsByTagName("body")[0];
    body.classList.remove("map-page");

    this.removeConnections();

    this.transportHubConnectionSubscription.unsubscribe();

    if (this.assignmentCreatedSubscription)
      this.assignmentCreatedSubscription.unsubscribe();

    if (this.assignmentTakenSubscription)
      this.assignmentTakenSubscription.unsubscribe();

    if (this.assignmentFinishedSubscription)
      this.assignmentFinishedSubscription.unsubscribe();
  }

  public fetchAssignments = ($event: { page, itemsPerPage }, notify: boolean = true): void => {
    let model = new FreeAssignmentsModel();

    this.currentPage = model.page = $event.page;
    this.currentPageSize = model.pageSize = $event.itemsPerPage;

    this.freeAssignments.fetch(model).subscribe(res => {
      if (res.successful) {
        this.freeAssignmentsList = res.data;
        this.totalItems = res.totalItems;

        if (notify && res.data.length > 0)
          this.notifications.showSuccess("Pobrano najnowszą listę wolnych zleceń.");
        else if (notify && res.data.length == 0)
          this.notifications.showInfo("Brak nowych zleceń.");

      } else {
        this.notifications.showError("Wystąpił problem przy pobieraniu listy wolnych zleceń.");
      }
    }, () => this.notifications.showError("Wystąpił problem przy pobieraniu listy wolnych zleceń."));
  };

  public refreshAssignmentsList = (): void => {
    this.fetchAssignments({ page: this.currentPage, itemsPerPage: this.currentPageSize });
  };

  public fetchCurrentAssignment = (): void => {
    let id = this.userManager.userId;
    this.currentAssignment = null;
    this.currentDriversAssignment.fetch(id).subscribe(res => {
      if (res.successful) {
        this.currentAssignment = res.data;

        this.notifications.showSuccess("Pobrano bieżące zlecenie.");
      }
    })
  };

  public fetchAssignmentDetails = (id: string): void => {
    this.assignmentDetailsService.fetch(id).subscribe(res => {
      if (res.successful) {
        this.assignmentDetails = res.data;
        this.assignmentDetailsModalRef = this.modalService.show(this.assignmentDetailsModal);
      } else {
        this.notifications.showError("Pobieranie informacji o zleceniu nie powiodło się.");
      }

      this.loader.hide();
    })
  };

  public takeAssignment = (id: string): void => {
    this.loader.show("Podejmowanie zlecenia...");

    this.takeAssignmentService.take(id).subscribe(res => {
      this.loader.hide();
      if (res.successful) {
        this.notifications.showSuccess("Przyjęto zlecenie!");

        this.transportHubService.assignmentTaken(id, this.freeAssignmentsList.find(x => x.id === id).title)
        this.refreshAssignmentsList();
        this.fetchCurrentAssignment();
      } else {
        this.notifications.showError("Przyjęcie zlecenia nie powiodło się.");
      }
    })
  };

  public finishCurrentAssignment = (): void => {
    this.loader.show("Kończenie bieżącego zlecenia...");

    let id = this.currentAssignment.id;

    this.finishAssignmentService.finish(this.currentAssignment.id).subscribe(res => {
      if (res.successful) {
        this.notifications.showSuccess("Ukończono zlecenie!");

        this.transportHubService.assignmentFinished(id, this.currentAssignment.title)
        this.fetchCurrentAssignment();
      } else {
        this.notifications.showError("Zakończenie zlecenia nie powiodło się.")
      }

      this.loader.hide();
    });
  };

  public downloadTransportDocument = (assignmentId: string): void => {
    this.loader.show("Pobieranie dokumentu przewozowego...");

    forkJoin(
      this.transportDocumentFile.download(assignmentId),
      this.transportDocumentInfo.fetch(assignmentId)).subscribe(res => {
        this.loader.hide();

        let file = res[0];
        let info = res[1];

        try {
          FileSaver.saveAs(file, info.data.number + '.' + info.data.name.split('.')[1]);
        } catch (err) {
          this.notifications.showError("Wystąpił problem przy pobieraniu dokumentu.");
        }
      }, () => this.notifications.showError("Wystąpił problem przy pobieraniu dokumentu."));
  };

  private initConnection = (): void => {
    if (this.locationsService.isDisconnected) {
      this.locationsService.connect();
    } else if (this.locationsService.isDisconnecting) {
      setTimeout(() => this.locationsService.connect(), 100);
    }

    if (this.transportHubService.isDisconnected) {
      this.transportHubService.connect();
    } else if (this.transportHubService.isDisconnecting) {
      setTimeout(() => this.transportHubService.connect(), 100);
    }
  };

  private removeConnections = ():void => {
    this.locationsService.disconnect(false);
    this.transportHubService.disconnect(false);
  }

  get hasActiveAssignment(): boolean {
    return this.currentAssignment && this.currentAssignment !== null && this.currentAssignment !== undefined;
  }

  public get destination(): string {
    return this.currentAssignment &&
      this.currentAssignment.destinationStreet +
      ", " +
      this.currentAssignment.destinationPostalCode +
      " " +
      this.currentAssignment.destinationCity +
      ", " +
      this.currentAssignment.destinationCountry;
  }

  public get start(): string {
    return this.currentAssignment &&
      this.currentAssignment.startingStreet +
      ", " +
      this.currentAssignment.startingPostalCode +
      " " +
      this.currentAssignment.startingCity +
      ", " +
      this.currentAssignment.startingCountry;
  }

  public get deadline(): Date {
    return new Date(Date.parse(this.currentAssignment.deadline.toString()));
  }
}
