import { Result } from 'src/infrastructure/interfaces/result';

export class UsersDetailsResult implements Result {
    message: string;
    successful: boolean;
    data: UserDetailsDTO[];
}

class UserDetailsDTO {
    firstName: string;
    lastName: string;
    email: string;
    phone: string;
}
