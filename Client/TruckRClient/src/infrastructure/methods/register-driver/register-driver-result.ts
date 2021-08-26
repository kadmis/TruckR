import { Result } from 'src/infrastructure/interfaces/result';

export class RegisterDriverResult implements Result {
    message: string;
    successful: boolean;
    id: string | null;
}
