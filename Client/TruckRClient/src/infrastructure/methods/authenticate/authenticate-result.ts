import { Result } from 'src/infrastructure/interfaces/result';

export class AuthenticateResult implements Result {
    message: string;
    successful: boolean;
    token: string;
    refreshToken: string | null;
    refreshInterval: number | null;
    role: string;
    userId: string;
}
