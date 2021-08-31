export class CreateAssignmentModel {
    title: string;
    description: string;

    document: File;

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
