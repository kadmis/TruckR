import { Result } from 'src/infrastructure/interfaces/result';

export class ActivationResult implements Result {
    message: string;
    successful: boolean;
}
