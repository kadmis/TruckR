import { Result } from 'src/infrastructure/interfaces/result';

export class UserDetailsResult implements Result {
    message: string;
    successful: boolean;
    details: UserDetailsDTO;
}

class UserDetailsDTO {
    firstName: string;
    lastName: string;
    email: string;
    phone: string;
}
