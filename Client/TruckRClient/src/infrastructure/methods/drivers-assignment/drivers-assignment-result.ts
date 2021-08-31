import { Result } from 'src/infrastructure/interfaces/result';

export class DriversAssignmentResult implements Result {
    message: string;
    successful: boolean;
    data: DriversAssignmentDTO;
}

export class DriversAssignmentDTO {
    id: string;

    title:string;
    description:string;

    dispatcher:string;

    destinationCountry:string;
    destinationCity:string;
    destinationStreet:string;
    destinationPostalCode:string;

    startingCountry:string;
    startingCity:string;
    startingStreet:string;
    startingPostalCode:string;

    deadline: Date;
    createdOn: string;
}
