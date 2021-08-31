import { PaginatedResult } from 'src/infrastructure/interfaces/result';

export class FreeAssignmentsResult implements PaginatedResult {
    totalItems: number;
    message: string;
    successful: boolean;
    data: FreeAssignmentDTO[];
}

export class FreeAssignmentDTO {
    id: string;
    title: string;
    from: string;
    to: string;
    createdDateUTC: string;
    deadlineUTC: string;
}
