import React, { useState } from 'react';
import { Table, Button, Spinner, Pagination } from 'react-bootstrap';
import { Starship } from '../types/Starship';
import { PagedResult, StarshipFilter } from '../types/StarshipFilter';

interface StarshipTableProps {
  starships: Starship[];
  loading: boolean;
  onEdit: (starship: Starship) => void;
  onDestroy: (id: number) => void;
  pagedResult?: PagedResult<Starship>;
  currentFilter?: StarshipFilter;
  onFilterChange?: (filter: StarshipFilter) => void;
}

const StarshipTable: React.FC<StarshipTableProps> = ({
  starships,
  loading,
  onEdit,
  onDestroy,
  pagedResult,
  currentFilter,
  onFilterChange,
}) => {
  const [destroyingId, setDestroyingId] = useState<number | null>(null);

  const handleDestroy = async (id: number) => {
    setDestroyingId(id);
    // Add a small delay to show the animation
    setTimeout(() => {
      onDestroy(id);
      setDestroyingId(null);
    }, 1000);
  };

  const handleSort = (column: string) => {
    if (!currentFilter || !onFilterChange) return;

    const newDirection = 
      currentFilter.sortBy === column && currentFilter.sortDirection === 'asc' 
        ? 'desc' 
        : 'asc';

    onFilterChange({
      ...currentFilter,
      sortBy: column,
      sortDirection: newDirection,
      pageNumber: 1
    });
  };

  const handlePageChange = (page: number) => {
    if (!currentFilter || !onFilterChange) return;

    onFilterChange({
      ...currentFilter,
      pageNumber: page
    });
  };

  const getSortIcon = (column: string) => {
    if (!currentFilter || currentFilter.sortBy !== column) return ' ↕️';
    return currentFilter.sortDirection === 'asc' ? ' ↑' : ' ↓';
  };

  const SortableHeader: React.FC<{ column: string; children: React.ReactNode }> = ({ column, children }) => (
    <th 
      style={{ cursor: 'pointer', userSelect: 'none' }}
      onClick={() => handleSort(column)}
      title={`Sort by ${children}`}
    >
      {children}{getSortIcon(column)}
    </th>
  );
  if (loading) {
    return (
      <div className="text-center">
        <Spinner animation="border" role="status">
          <span className="visually-hidden">Loading...</span>
        </Spinner>
      </div>
    );
  }

  if (starships.length === 0) {
    return (
      <div className="text-center">
        <p>No starships found. Try seeding from SWAPI or adding a new one.</p>
      </div>
    );
  }

  return (
    <>
      <div className="table-responsive">
        <Table striped bordered hover>
          <thead>
            <tr>
              <SortableHeader column="name">Name</SortableHeader>
              <SortableHeader column="model">Model</SortableHeader>
              <SortableHeader column="manufacturer">Manufacturer</SortableHeader>
              <SortableHeader column="starshipClass">Class</SortableHeader>
              <SortableHeader column="length">Length</SortableHeader>
              <SortableHeader column="crew">Crew</SortableHeader>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {starships.map((starship) => (
              <tr 
                key={starship.id}
                className={destroyingId === starship.id ? 'destroying' : ''}
              >
                <td>{starship.name}</td>
                <td>{starship.model}</td>
                <td>{starship.manufacturer}</td>
                <td>{starship.starshipClass}</td>
                <td>{starship.length}</td>
                <td>{starship.crew}</td>
                <td>
                  <div className="d-flex gap-2">
                    <Button
                      variant="outline-primary"
                      size="sm"
                      onClick={() => onEdit(starship)}
                      disabled={destroyingId === starship.id}
                    >
                      Edit
                    </Button>
                    <Button
                      variant="outline-danger"
                      size="sm"
                      onClick={() => handleDestroy(starship.id)}
                      disabled={destroyingId === starship.id}
                    >
                      {destroyingId === starship.id ? '💥 Destroying...' : 'Destroy'}
                    </Button>
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
      </div>

      {/* Pagination */}
      {pagedResult && pagedResult.totalPages > 1 && (
        <div className="d-flex justify-content-between align-items-center mt-3">
          <div>
            <small className="text-muted">
              Showing {((pagedResult.pageNumber - 1) * pagedResult.pageSize) + 1} to{' '}
              {Math.min(pagedResult.pageNumber * pagedResult.pageSize, pagedResult.totalCount)} of{' '}
              {pagedResult.totalCount} starships
            </small>
          </div>
          <Pagination className="mb-0">
            <Pagination.First 
              onClick={() => handlePageChange(1)}
              disabled={!pagedResult.hasPreviousPage}
            />
            <Pagination.Prev 
              onClick={() => handlePageChange(pagedResult.pageNumber - 1)}
              disabled={!pagedResult.hasPreviousPage}
            />
            
            {/* Show page numbers */}
            {Array.from({ length: Math.min(5, pagedResult.totalPages) }, (_, i) => {
              const startPage = Math.max(1, pagedResult.pageNumber - 2);
              const pageNum = startPage + i;
              if (pageNum > pagedResult.totalPages) return null;
              
              return (
                <Pagination.Item
                  key={pageNum}
                  active={pageNum === pagedResult.pageNumber}
                  onClick={() => handlePageChange(pageNum)}
                >
                  {pageNum}
                </Pagination.Item>
              );
            })}
            
            <Pagination.Next 
              onClick={() => handlePageChange(pagedResult.pageNumber + 1)}
              disabled={!pagedResult.hasNextPage}
            />
            <Pagination.Last 
              onClick={() => handlePageChange(pagedResult.totalPages)}
              disabled={!pagedResult.hasNextPage}
            />
          </Pagination>
        </div>
      )}
    </>
  );
};

export default StarshipTable;
