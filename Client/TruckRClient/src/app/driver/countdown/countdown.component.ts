import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Subscription, interval } from 'rxjs';

@Component({
  selector: 'app-countdown',
  templateUrl: './countdown.component.html',
  styleUrls: ['./countdown.component.scss']
})
export class CountdownComponent implements OnInit, OnDestroy {

  @Input() countdownLabel: string;
  @Input() dDay: Date;

  private subscription: Subscription;
  
  public dateNow: Date = new Date();

  private milliSecondsInASecond = 1000;
  private hoursInADay = 24;
  private minutesInAnHour = 60;
  private secondsInAMinute  = 60;

  public timeDifference;
  public secondsToDday;
  public minutesToDday;
  public hoursToDday;
  public daysToDday;

  constructor() { }

  ngOnInit(): void {
    this.subscription = interval(1000).subscribe(x => { this.getTimeDifference(); });
  }


  ngOnDestroy():void {
    this.subscription.unsubscribe();
  }

  private getTimeDifference () {
    if(this.dDay) {
      this.timeDifference = this.dDay.getTime() - new Date().getTime();
      this.allocateTimeUnits(this.timeDifference);
    }
  }

  private allocateTimeUnits (timeDifference) {
    this.secondsToDday = Math.floor((timeDifference) / (this.milliSecondsInASecond) % this.secondsInAMinute);
    this.minutesToDday = Math.floor((timeDifference) / (this.milliSecondsInASecond * this.minutesInAnHour) % this.secondsInAMinute);
    this.hoursToDday = Math.floor((timeDifference) / (this.milliSecondsInASecond * this.minutesInAnHour * this.secondsInAMinute) % this.hoursInADay);
    this.daysToDday = Math.floor((timeDifference) / (this.milliSecondsInASecond * this.minutesInAnHour * this.secondsInAMinute * this.hoursInADay));
  }
}
