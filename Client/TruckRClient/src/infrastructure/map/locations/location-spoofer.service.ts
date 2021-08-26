import { Injectable } from '@angular/core';
import { Coordinates } from './coordinates';

@Injectable({
  providedIn: 'root'
})
export class LocationSpooferService {

  private spoofedPath = [
    new Coordinates(50.39125287123264, 18.952261487581307),
    new Coordinates(50.392700495393356, 18.952474362851998),
    new Coordinates(50.3927432197405, 18.95259262689127),
    new Coordinates(50.392682903003944, 18.95301837743265),
    new Coordinates(50.39251451837493, 18.953649118975434),
    new Coordinates(50.39237126530931, 18.95411429086324),
    new Coordinates(50.39213250923805, 18.95459128915497),
    new Coordinates(50.39192391084437, 18.954894833522435)
  ];
  private iterator: number = 0;
  private routeBack:boolean = false;
  private geolocate = require('mock-geolocation');

  constructor() { };

  public provideSpoofedLocations = (coordinates: Coordinates[]):void => {
    this.spoofedPath = coordinates;
  }

  public useFakeGeolocator = ():void => {
    this.geolocate.use();
  };

  public restoreDefaultGeolocator = ():void => {
    this.geolocate.restore();
  };

  public startSendingFakeGeoLocations = ():void => {
    setInterval(()=>{
      let pos = this.next();
      console.log('[LocationSpoofer] Sending fake coordinates: ['+pos.latitude+', '+pos.longitude+']');
      this.geolocate.change({lat: pos.latitude, lng: pos.longitude});
    },15*1000);
  };

  private next = ():Coordinates => {
    let next: Coordinates;

    if(this.iterator == this.spoofedPath.length-1) {
      this.routeBack = true;
    } 

    if(this.iterator == 0) {
      this.routeBack = false;
    }

    if(this.routeBack === true) {
      next = this.spoofedPath[this.iterator--];
    } else {
      next = this.spoofedPath[this.iterator++];
    }
    
    return next;
  };
}
