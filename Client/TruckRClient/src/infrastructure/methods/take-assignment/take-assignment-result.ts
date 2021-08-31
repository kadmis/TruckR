import { Result } from 'src/infrastructure/interfaces/result';

export class TakeAssignmentResult implements Result {
    message: string;
    successful: boolean;
}
