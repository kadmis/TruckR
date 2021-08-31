export interface Result {
    message: string;
    successful: boolean;
}

export interface PaginatedResult extends Result {
    totalItems: number;
}
