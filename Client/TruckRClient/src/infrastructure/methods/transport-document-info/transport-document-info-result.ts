import { Result } from 'src/infrastructure/interfaces/result';

export class TransportDocumentInfoResult implements Result {
    message: string;
    successful: boolean;
    data: TransportDocumentInfoDTO;
}

export class TransportDocumentInfoDTO {
    name: string;
    number: string;
}
