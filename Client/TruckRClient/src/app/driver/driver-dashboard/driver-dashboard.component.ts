import { Component, OnInit } from '@angular/core';
import { LocationsService } from 'src/infrastructure/map/locations/locations.service';

@Component({
  selector: 'app-driver-dashboard',
  templateUrl: './driver-dashboard.component.html',
  styleUrls: ['./driver-dashboard.component.scss']
})
export class DriverDashboardComponent implements OnInit {

  constructor(private locationsService: LocationsService) { }

  ngOnInit(): void {
    this.initConnection();
  }

  private initConnection = ():void => {
    this.locationsService.connect();
    this.locationsService.$connected.subscribe(res=>{
      if(res) {
        this.locationsService.startWatchingSpoofedLocation();
      }
    })
  }
}
