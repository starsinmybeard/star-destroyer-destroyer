import React, { useState, useEffect } from 'react';
import { Row, Col, Form, Button, Card } from 'react-bootstrap';
import { StarshipFilter } from '../types/StarshipFilter';
import { starshipService } from '../services/starshipService';

interface StarshipFiltersProps {
  onFilterChange: (filter: StarshipFilter) => void;
  loading: boolean;
}

const StarshipFilters: React.FC<StarshipFiltersProps> = ({ onFilterChange, loading }) => {
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedClass, setSelectedClass] = useState('');
  const [selectedManufacturer, setSelectedManufacturer] = useState('');
  const [minLength, setMinLength] = useState('');
  const [maxLength, setMaxLength] = useState('');
  const [sortBy, setSortBy] = useState('name');
  const [sortDirection, setSortDirection] = useState<'asc' | 'desc'>('asc');
  
  const [starshipClasses, setStarshipClasses] = useState<string[]>([]);
  const [manufacturers, setManufacturers] = useState<string[]>([]);

  useEffect(() => {
    loadFilterOptions();
  }, []);

  const loadFilterOptions = async () => {
    try {
      const [classes, mfrs] = await Promise.all([
        starshipService.getStarshipClasses(),
        starshipService.getManufacturers()
      ]);
      setStarshipClasses(classes);
      setManufacturers(mfrs);
    } catch (error) {
      console.error('Failed to load filter options:', error);
    }
  };

  const handleFilterSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    const filter: StarshipFilter = {
      searchTerm: searchTerm || undefined,
      starshipClass: selectedClass || undefined,
      manufacturer: selectedManufacturer || undefined,
      minLength: minLength ? parseInt(minLength) : undefined,
      maxLength: maxLength ? parseInt(maxLength) : undefined,
      sortBy,
      sortDirection,
      pageNumber: 1,
      pageSize: 50
    };

    onFilterChange(filter);
  };

  const handleClearFilters = () => {
    setSearchTerm('');
    setSelectedClass('');
    setSelectedManufacturer('');
    setMinLength('');
    setMaxLength('');
    setSortBy('name');
    setSortDirection('asc');
    
    onFilterChange({
      sortBy: 'name',
      sortDirection: 'asc',
      pageNumber: 1,
      pageSize: 50
    });
  };

  return (
    <Card className="mb-4">
      <Card.Header>
        <h5 className="mb-0">🔍 Filter & Sort Starships</h5>
      </Card.Header>
      <Card.Body>
        <Form onSubmit={handleFilterSubmit}>
          <Row className="mb-3 align-items-end">
            <Col md={3}>
              <Form.Group>
                <Form.Label className="small">Search</Form.Label>
                <Form.Control
                  size="sm"
                  type="text"
                  placeholder="Search..."
                  value={searchTerm}
                  onChange={(e) => setSearchTerm(e.target.value)}
                />
              </Form.Group>
            </Col>
            <Col md={2}>
              <Form.Group>
                <Form.Label className="small">Class</Form.Label>
                <Form.Select 
                  size="sm"
                  value={selectedClass} 
                  onChange={(e) => setSelectedClass(e.target.value)}
                >
                  <option value="">All</option>
                  {starshipClasses.map(cls => (
                    <option key={cls} value={cls}>{cls}</option>
                  ))}
                </Form.Select>
              </Form.Group>
            </Col>
            <Col md={2}>
              <Form.Group>
                <Form.Label className="small">Manufacturer</Form.Label>
                <Form.Select 
                  size="sm"
                  value={selectedManufacturer} 
                  onChange={(e) => setSelectedManufacturer(e.target.value)}
                >
                  <option value="">All</option>
                  {manufacturers.map(mfr => (
                    <option key={mfr} value={mfr}>{mfr.length > 15 ? mfr.substring(0, 15) + '...' : mfr}</option>
                  ))}
                </Form.Select>
              </Form.Group>
            </Col>
            <Col md={1}>
              <Form.Group>
                <Form.Label className="small">Min Length</Form.Label>
                <Form.Control
                  size="sm"
                  type="number"
                  placeholder="0"
                  value={minLength}
                  onChange={(e) => setMinLength(e.target.value)}
                />
              </Form.Group>
            </Col>
            <Col md={1}>
              <Form.Group>
                <Form.Label className="small">Max Length</Form.Label>
                <Form.Control
                  size="sm"
                  type="number"
                  placeholder="∞"
                  value={maxLength}
                  onChange={(e) => setMaxLength(e.target.value)}
                />
              </Form.Group>
            </Col>
            <Col md={1}>
              <Form.Group>
                <Form.Label className="small">Sort By</Form.Label>
                <Form.Select 
                  size="sm"
                  value={sortBy} 
                  onChange={(e) => setSortBy(e.target.value)}
                >
                  <option value="name">Name</option>
                  <option value="model">Model</option>
                  <option value="manufacturer">Mfr.</option>
                  <option value="starshipClass">Class</option>
                  <option value="length">Length</option>
                  <option value="crew">Crew</option>
                </Form.Select>
              </Form.Group>
            </Col>
            <Col md={1}>
              <Form.Group>
                <Form.Label className="small">Direction</Form.Label>
                <Form.Select 
                  size="sm"
                  value={sortDirection} 
                  onChange={(e) => setSortDirection(e.target.value as 'asc' | 'desc')}
                >
                  <option value="asc">↑ Asc</option>
                  <option value="desc">↓ Desc</option>
                </Form.Select>
              </Form.Group>
            </Col>
            <Col md={1}>
              <div className="d-flex flex-column gap-1">
                <Button 
                  type="submit" 
                  variant="primary"
                  size="sm"
                  disabled={loading}
                >
                  {loading ? '...' : 'Filter'}
                </Button>
                <Button 
                  type="button" 
                  variant="outline-secondary"
                  size="sm"
                  onClick={handleClearFilters}
                  disabled={loading}
                >
                  Clear
                </Button>
              </div>
            </Col>
          </Row>
        </Form>
      </Card.Body>
    </Card>
  );
};

export default StarshipFilters;
