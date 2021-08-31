import { Result } from 'src/infrastructure/interfaces/result';

export class AssignmentDetailsResult implements Result {
    message: string;
    successful: boolean;
    data: AssignmentDetailsDTO; 
}

export class AssignmentDetailsDTO {
    title: string;
    description: string;

    startingCountry: string;
    startingCity: string;
    startingStreet: string;
    startingPostalCode: string;

    destinationCountry: string;
    destinationCity: string;
    destinationStreet: string;
    destinationPostalCode: string;

    deadline: Date;
}
