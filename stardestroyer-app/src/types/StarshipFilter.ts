export interface StarshipFilter {
  searchTerm?: string;
  starshipClass?: string;
  manufacturer?: string;
  minLength?: number;
  maxLength?: number;
  sortBy?: string;
  sortDirection?: 'asc' | 'desc';
  pageNumber?: number;
  pageSize?: number;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}
