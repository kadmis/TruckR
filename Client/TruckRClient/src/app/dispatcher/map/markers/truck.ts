import * as L from 'leaflet';
import {Icon} from 'leaflet';

export class Truck {
    static Normal: Icon = L.icon({
        iconUrl: 'assets/img/car.svg',
    
        iconSize:     [48, 48], // size of the icon
        iconAnchor:   [24, 48], // point of the icon which will correspond to marker's location
        popupAnchor:  [24, 48] // point from which the popup should open relative to the iconAnchor
    });

    static Highlighted: Icon = L.icon({
        iconUrl: 'assets/img/car-highlighted.svg',
    
        iconSize:     [48, 48], // size of the icon
        iconAnchor:   [24, 48], // point of the icon which will correspond to marker's location
        popupAnchor:  [24, 48] // point from which the popup should open relative to the iconAnchor
    });

    static HighlightedRed: Icon = L.icon({
        iconUrl: 'assets/img/car-highlighted-red.svg',
    
        iconSize:     [48, 48], // size of the icon
        iconAnchor:   [24, 48], // point of the icon which will correspond to marker's location
        popupAnchor:  [24, 48] // point from which the popup should open relative to the iconAnchor
    });

    static NormalColor: string = "black";
    static HightlightColor: string = "#3358f4";
    static HightlightColorRed: string = "#fd5d93";
}
