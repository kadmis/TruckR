import { Result } from 'src/infrastructure/interfaces/result';

export class FinishAssignmentResult implements Result {
    message: string;
    successful: boolean;
}
