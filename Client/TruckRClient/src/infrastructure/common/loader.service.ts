import { Injectable } from '@angular/core';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  public shown: boolean = false;

  constructor(private notifications: NotificationService) { }

  show = (text?:string):void => {
    this.notifications.showLoading(text);
    this.shown = true;
  }

  hide = ():void => {
    this.notifications.closeLoading();
    this.shown = false;
  }
}
