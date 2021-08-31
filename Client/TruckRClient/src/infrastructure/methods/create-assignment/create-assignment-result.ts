import { Result } from 'src/infrastructure/interfaces/result';

export class CreateAssignmentResult implements Result {
    message: string;
    successful: boolean;
    assignmentId: string;
}
