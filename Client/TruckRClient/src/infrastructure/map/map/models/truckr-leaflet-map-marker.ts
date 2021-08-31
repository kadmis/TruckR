import * as L from 'leaflet';
import { Coordinates } from '../../locations/models/coordinates';

export class TruckrLeafletMapMarker {
    id: string;
    marker: L.Marker;
    path: L.Polyline[] = new Array<L.Polyline>();

    currentLocation: Coordinates;

    currentColor: string;

    public setPathColor = (color:string):void => {
        this.path.forEach(polyline=>polyline.setStyle({color: color}));
    }

    public setIcon = (icon:L.Icon):void => {
        this.marker.setIcon(icon);
    }

    public slicePath = (zoomLevel: number):Array<L.Polyline> => {
        let length = zoomLevel - 3;
        if(this.path.length > length) {
            return this.path.splice(0,length-1);
        }
        return [];
    }
}
