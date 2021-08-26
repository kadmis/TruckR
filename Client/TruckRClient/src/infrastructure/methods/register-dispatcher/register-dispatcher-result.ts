import { Result } from 'src/infrastructure/interfaces/result';

export class RegisterDispatcherResult implements Result {
    message: string;
    successful: boolean;
    id: string | null;
}
