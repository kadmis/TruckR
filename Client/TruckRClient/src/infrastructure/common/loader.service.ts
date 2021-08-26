import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-bootstrap-spinner';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  public shown: boolean = false;
  public loadingText: string;

  constructor(private notifications: NotificationService) { }

  show = ():void => {
    this.notifications.showLoading(this.loadingText);
    this.shown = true;
  }

  hide = ():void => {
    this.notifications.closeLoading();
    this.shown = false;
  }
}
