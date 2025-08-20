import React, { useState, useEffect } from 'react';
import { Container, Navbar, Button, Alert } from 'react-bootstrap';
import StarshipTable from './components/StarshipTable';
import StarshipModal from './components/StarshipModal';
import StarshipFilters from './components/StarshipFilters';
import { Starship } from './types/Starship';
import { StarshipFilter, PagedResult } from './types/StarshipFilter';
import { starshipService } from './services/starshipService';
import './App.css';

function App() {
  const [starships, setStarships] = useState<Starship[]>([]);
  const [pagedResult, setPagedResult] = useState<PagedResult<Starship> | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [showModal, setShowModal] = useState(false);
  const [editingStarship, setEditingStarship] = useState<Starship | null>(null);
  const [currentFilter, setCurrentFilter] = useState<StarshipFilter>({
    sortBy: 'name',
    sortDirection: 'asc',
    pageNumber: 1,
    pageSize: 50
  });
  const [useFiltering, setUseFiltering] = useState(false);

  useEffect(() => {
    if (useFiltering) {
      loadFilteredStarships(currentFilter);
    } else {
      loadStarships();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [useFiltering]);

  const loadStarships = async () => {
    try {
      setLoading(true);
      const data = await starshipService.getAllStarships();
      setStarships(data);
      setPagedResult(null);
      setError(null);
    } catch (err) {
      setError('Failed to load starships. Make sure the API is running.');
    } finally {
      setLoading(false);
    }
  };

  const loadFilteredStarships = async (filter: StarshipFilter) => {
    try {
      setLoading(true);
      const result = await starshipService.getFilteredStarships(filter);
      setStarships(result.items);
      setPagedResult(result);
      setCurrentFilter(filter);
      setError(null);
    } catch (err) {
      setError('Failed to load filtered starships. Make sure the API is running.');
    } finally {
      setLoading(false);
    }
  };

  const handleCreate = () => {
    setEditingStarship(null);
    setShowModal(true);
  };

  const handleEdit = (starship: Starship) => {
    setEditingStarship(starship);
    setShowModal(true);
  };

  const handleDestroy = async (id: number) => {
    if (window.confirm('Are you sure you want to destroy this starship?')) {
      try {
        await starshipService.deleteStarship(id);
        if (useFiltering) {
          await loadFilteredStarships(currentFilter);
        } else {
          await loadStarships();
        }
      } catch (err) {
        setError('Failed to destroy starship');
      }
    }
  };

  const handleSave = async (starship: Omit<Starship, 'id'> | Starship) => {
    try {
      if (editingStarship) {
        await starshipService.updateStarship(editingStarship.id, starship as Starship);
      } else {
        await starshipService.createStarship(starship as Omit<Starship, 'id'>);
      }
      setShowModal(false);
      if (useFiltering) {
        await loadFilteredStarships(currentFilter);
      } else {
        await loadStarships();
      }
    } catch (err) {
      setError('Failed to save starship');
    }
  };

  const handleSeedFromSwapi = async () => {
    try {
      setLoading(true);
      await starshipService.seedFromSwapi();
      if (useFiltering) {
        await loadFilteredStarships(currentFilter);
      } else {
        await loadStarships();
      }
      setError(null);
    } catch (err) {
      setError('Failed to seed data from SWAPI');
    } finally {
      setLoading(false);
    }
  };

  const handleFilterChange = (filter: StarshipFilter) => {
    setUseFiltering(true);
    loadFilteredStarships(filter);
  };

  const handleShowAllStarships = () => {
    setUseFiltering(false);
    loadStarships();
  };

  return (
    <div className="App">
      <Navbar bg="dark" variant="dark" expand="lg">
        <Container>
          <Navbar.Brand href="#home" className="d-flex align-items-center">
            <span style={{ fontSize: '1.5rem', marginRight: '0.5rem' }}>🌑</span>
            Star Destroyer Destroyer
          </Navbar.Brand>
        </Container>
      </Navbar>

      <Container className="my-4">
        {error && (
          <Alert variant="danger" dismissible onClose={() => setError(null)}>
            {error}
          </Alert>
        )}

        <div className="d-flex justify-content-between align-items-center mb-3">
          <h2>Starships</h2>
          <div>
            {useFiltering && (
              <Button 
                variant="outline-secondary" 
                onClick={handleShowAllStarships}
                disabled={loading}
                className="me-2"
              >
                Show All Starships
              </Button>
            )}
            <Button 
              variant="outline-primary" 
              onClick={handleSeedFromSwapi}
              disabled={loading}
              className="me-2"
            >
              Pull data from the Star Wars API
            </Button>
            <Button variant="primary" onClick={handleCreate}>
              Add Starship
            </Button>
          </div>
        </div>

        <StarshipFilters 
          onFilterChange={handleFilterChange}
          loading={loading}
        />

        <StarshipTable
          starships={starships}
          loading={loading}
          onEdit={handleEdit}
          onDestroy={handleDestroy}
          pagedResult={pagedResult || undefined}
          currentFilter={useFiltering ? currentFilter : undefined}
          onFilterChange={useFiltering ? handleFilterChange : undefined}
        />

        <StarshipModal
          show={showModal}
          onHide={() => setShowModal(false)}
          onSave={handleSave}
          starship={editingStarship}
        />
      </Container>
    </div>
  );
}

export default App;
