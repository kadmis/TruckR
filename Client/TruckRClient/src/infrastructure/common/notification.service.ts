import { Injectable } from '@angular/core';
import { ToastrService, IndividualConfig, ActiveToast } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private config: Partial<IndividualConfig>;
  private successTitle: string = "Sukces";
  private errorTitle: string = "Błąd";
  private warningTitle: string = "Ostrzeżenie";
  private infoTitle: string = "Informacja";

  private loadingConfig: Partial<IndividualConfig>;
  private loadingTitle: string = "Ładowanie...";
  private loadingId: number;

  private validationErrorConfig: Partial<IndividualConfig>;
  private validationErrorTitle: string = "Błąd walidacji";

  constructor(private toastr: ToastrService) {
    this.config = {
      positionClass: 'toast-bottom-right',
      closeButton: true,
      progressBar: true
    };

    this.loadingConfig = {
      positionClass: 'toast-bottom-right',
      closeButton: false,
      progressBar: false,
      disableTimeOut: true,
      tapToDismiss: false
    };

    this.validationErrorConfig = {
      positionClass: 'toast-top-right',
      closeButton: true,
      progressBar: false,
      disableTimeOut: true,
      tapToDismiss: true
    };
   }

  showSuccess = (message: string, title?: string):void => {
    this.toastr.success(message, title ? title : this.successTitle, this.config);
  }

  showError = (message: string, title?: string):void => {
    this.toastr.error(message, title ? title : this.errorTitle, this.config);
  }

  showWarning = (message: string, title?: string):void => {
    this.toastr.warning(message, title ? title : this.warningTitle, this.config);
  }

  showInfo = (message: string, title?: string):void => {
    this.toastr.info(message, title ? title : this.infoTitle, this.config);
  }

  showLoading = (message?: string):void => {
    this.loadingId = this.toastr.info(message ? message : this.loadingTitle, "", this.loadingConfig).toastId;
  }

  showValidationError = (message:string):void => {
    this.toastr.error(message, this.validationErrorTitle, this.validationErrorConfig);
  }

  closeLoading = ():void => {
    this.toastr.clear(this.loadingId);
  }
}
