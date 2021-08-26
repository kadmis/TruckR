import { Coordinates } from './coordinates';

export class UserLocation {
    public coordinates: Coordinates;

    public constructor(public userId: string, longitude: number, latitude: number){
        this.userId = userId;
        this.coordinates = new Coordinates(latitude, longitude);
    }
}
