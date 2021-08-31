import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { CreateAssignmentService } from 'src/infrastructure/methods/create-assignment/create-assignment.service';
import { LoaderService } from 'src/infrastructure/common/loader.service';
import { NotificationService } from 'src/infrastructure/common/notification.service';
import { FormGroup, Validators, FormBuilder, AbstractControl } from '@angular/forms';
import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { CreateAssignmentModel } from 'src/infrastructure/methods/create-assignment/create-assignment-model';
import { Subject, Observable } from 'rxjs';
import { AssignmentEventsService } from 'src/infrastructure/assignments/assignment-events.service';
import { Helper } from 'src/infrastructure/common/helper';
import { DatePipe } from '@angular/common';
import { listLocales } from 'ngx-bootstrap/chronos';


@Component({
  selector: 'app-create-assignment',
  templateUrl: './create-assignment.component.html',
  styleUrls: ['./create-assignment.component.scss']
})
export class CreateAssignmentComponent implements OnInit {

  @Output() closed: EventEmitter<boolean>;

  focusTitle: boolean;
  focusDescription: boolean;
  
  focusStartingCountry: boolean;
  focusStartingCity: boolean;
  focuStartingStreet: boolean;
  focusStartingPostalCode: boolean;

  focusDestinationCountry: boolean;
  focusDestinationCity: boolean;
  focusDestinationStreet: boolean;
  focusDestinationPostalCode: boolean;

  focusDeadline: boolean;

  private document: File;
  documentValid: boolean;
  private validDocumentExtensions = ["pdf", "docx"];
  documentName: string;

  minDate: Date = Helper.addDays(new Date(Date.now()), 17);
  datePickerConfig: Partial<BsDatepickerConfig>;

  constructor(
    private formBuilder: FormBuilder,
    private createAssignment: CreateAssignmentService,
    private loader: LoaderService,
    private notifications: NotificationService,
    private assignmentEvents: AssignmentEventsService,
    private localeService: BsLocaleService) {
      this.closed = new EventEmitter<boolean>();
    }

  ngOnInit(): void {
    this.minDate = Helper.addDays(new Date(Date.now()), 1);
    this.datePickerConfig = {
      isAnimated: true,
      containerClass: 'theme-red',
      minDate: this.minDate,
      dateInputFormat: 'DD.MM.YYYY'
    };
    this.initForm();
  }

  form: FormGroup;

  initForm = ():void => {
    this.form = this.formBuilder.group({
      title: ["", [Validators.required]],
      description: ["", [Validators.required]],

      startingCountry: ["", [Validators.required]],
      startingCity: ["", [Validators.required]],
      startingPostalCode: ["", [Validators.required]],
      startingStreet: ["", [Validators.required]],

      destinationCountry: ["", [Validators.required]],
      destinationCity: ["", [Validators.required]],
      destinationPostalCode: ["", [Validators.required]],
      destinationStreet: ["", [Validators.required]],

      deadline: [this.minDate.toLocaleString(), [Validators.required]]
    });
  };

  confirm = ():void => {
    if(this.validateForm()) {
      this.loader.show("Tworzenie zlecenia w systemie...");

      this.createAssignment.create(this.modelFromForm).subscribe(res=>{
        if(res.successful) {
          this.notifications.showSuccess("Utworzono zlecenie!");
          this.initForm();
          this.assignmentEvents.assignmentCreated(res.assignmentId);
        } else {
          this.notifications.showError("Utworzenie zlecenia nie powiodło się.");
        }
  
        this.loader.hide();
      },err=> { 
        this.loader.hide();
        this.notifications.showError("Utworzenie zlecenia nie powiodło się. "+err.message);
      });
    }
  };

  close = ():void=> {
    this.closed.next(true);
  };

  onDocumentSelected = (files: FileList, input: HTMLInputElement):void => {

    let filesArray = Array.from(files);

    this.documentValid = false;

    let tooBig = false;
    let invalidExtension = false;

    filesArray.forEach(file=> {
      const size = file.size / 1024 / 1024;
      if(size > 5) {
        tooBig = true;
      }
      const extension = file.name.split('.').pop();
  
      if(extension && !this.validDocumentExtensions.includes(extension.toLowerCase())) {
        invalidExtension = true;
      }
    })

    if(tooBig) {
      this.clearDocumentInputValue(input);
      this.showDocumentTooBig();
    } else if(invalidExtension) {
      this.clearDocumentInputValue(input);
      this.showInvalidDocumentExtension();
    } else {
      this.document = filesArray[0];
      this.documentValid = true;
      this.documentName = this.document.name;
    }
  }

  private showDocumentTooBig = ():void => {
    this.notifications.showValidationError("Wybrany plik jest za duży.");
  }

  private showInvalidDocumentExtension = ():void => {
    let validFormatsString = "";
    for(let i=0;i<this.validDocumentExtensions.length;i++) {
      validFormatsString += this.validDocumentExtensions[i];
      if(i<this.validDocumentExtensions.length-1) {
        validFormatsString += ", ";
      }
    }
    this.notifications.showValidationError("Nieprawidłowy format pliku. Prawidłowe formaty: " + validFormatsString + ".");
  }

  private clearDocumentInputValue = (input: HTMLInputElement):void => {
    input.value = null;
  }

  private validateForm = ():boolean => {
    if(!this.isFormValid) {
      this.notifications.showValidationError("Proszę sprawdzić poprawność wypełnionego formularza.");
    }

    return this.isFormValid;
  };

  public touchedAndValid = (control:AbstractControl):boolean => {
    return control && control.touched && control.valid;
  }
  public touchedAndInvalid = (control:AbstractControl):boolean => {
    return control && control.touched && !control.valid;
  }

  get title() {
    return this.form && this.form.controls && this.form.controls.title;
  }

  get description() {
    return this.form && this.form.controls && this.form.controls.description;
  }

  get startingCountry() {
    return this.form && this.form.controls && this.form.controls.startingCountry;
  }
  get startingCity() {
    return this.form && this.form.controls && this.form.controls.startingCity;
  }
  get startingPostalCode() {
    return this.form && this.form.controls && this.form.controls.startingPostalCode;
  }
  get startingStreet() {
    return this.form && this.form.controls && this.form.controls.startingStreet;
  }

  get destinationCountry() {
    return this.form && this.form.controls && this.form.controls.destinationCountry;
  }
  get destinationCity() {
    return this.form && this.form.controls && this.form.controls.destinationCity;
  }
  get destinationPostalCode() {
    return this.form && this.form.controls && this.form.controls.destinationPostalCode;
  }
  get destinationStreet() {
    return this.form && this.form.controls && this.form.controls.destinationStreet;
  }

  get deadline() {
    return this.form && this.form.controls && this.form.controls.deadline;
  }

  get isFormValid ():boolean {
    return this.form && 
      this.title.touched && this.title.valid &&
      this.description.touched && this.description.valid &&
      this.startingCountry.touched && this.startingCountry.valid &&
      this.startingCity.touched && this.startingCity.valid &&
      this.startingStreet.touched && this.startingStreet.valid &&
      this.startingPostalCode.touched && this.startingPostalCode.valid &&
      this.destinationCountry.touched && this.destinationCountry.valid &&
      this.destinationCity.touched && this.destinationCity.valid &&
      this.destinationPostalCode.touched && this.destinationPostalCode.valid &&
      this.destinationStreet.touched && this.destinationStreet.valid &&
      this.documentValid;
  }

  get modelFromForm():CreateAssignmentModel {
    let model = new CreateAssignmentModel();

    model.title = this.title.value;
    model.description = this.description.value;

    model.startingCountry = this.startingCountry.value;
    model.startingCity = this.startingCity.value;
    model.startingPostalCode = this.startingPostalCode.value;
    model.startingStreet = this.startingStreet.value;

    model.destinationCountry = this.destinationCountry.value;
    model.destinationCity = this.destinationCity.value;
    model.destinationPostalCode = this.destinationPostalCode.value;
    model.destinationStreet = this.destinationStreet.value;

    model.document = this.document;
    model.deadline = this.deadline.value as Date;

    return model;
  }
}
