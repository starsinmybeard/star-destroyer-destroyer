import React, { useState, useEffect } from 'react';
import { Modal, Button, Form, Row, Col } from 'react-bootstrap';
import { Starship } from '../types/Starship';

interface StarshipModalProps {
  show: boolean;
  onHide: () => void;
  onSave: (starship: Omit<Starship, 'id'> | Starship) => void;
  starship: Starship | null;
}

const StarshipModal: React.FC<StarshipModalProps> = ({
  show,
  onHide,
  onSave,
  starship,
}) => {
  const [formData, setFormData] = useState({
    name: '',
    model: '',
    manufacturer: '',
    costInCredits: '',
    length: '',
    maxAtmospheringSpeed: '',
    crew: '',
    passengers: '',
    cargoCapacity: '',
    consumables: '',
    hyperdriveRating: '',
    mglt: '',
    starshipClass: '',
    url: '',
  });

  useEffect(() => {
    if (starship) {
      setFormData({
        name: starship.name,
        model: starship.model,
        manufacturer: starship.manufacturer,
        costInCredits: starship.costInCredits,
        length: starship.length,
        maxAtmospheringSpeed: starship.maxAtmospheringSpeed,
        crew: starship.crew,
        passengers: starship.passengers,
        cargoCapacity: starship.cargoCapacity,
        consumables: starship.consumables,
        hyperdriveRating: starship.hyperdriveRating,
        mglt: starship.mglt,
        starshipClass: starship.starshipClass,
        url: starship.url,
      });
    } else {
      setFormData({
        name: '',
        model: '',
        manufacturer: '',
        costInCredits: '',
        length: '',
        maxAtmospheringSpeed: '',
        crew: '',
        passengers: '',
        cargoCapacity: '',
        consumables: '',
        hyperdriveRating: '',
        mglt: '',
        starshipClass: '',
        url: '',
      });
    }
  }, [starship]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    const starshipData = {
      ...formData,
      created: starship?.created || new Date().toISOString(),
      edited: new Date().toISOString(),
    };

    if (starship) {
      onSave({ ...starshipData, id: starship.id });
    } else {
      onSave(starshipData);
    }
  };

  return (
    <Modal show={show} onHide={onHide} size="lg">
      <Form onSubmit={handleSubmit}>
        <Modal.Header closeButton>
          <Modal.Title>
            {starship ? 'Edit Starship' : 'Add New Starship'}
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Row>
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>Name *</Form.Label>
                <Form.Control
                  type="text"
                  name="name"
                  value={formData.name}
                  onChange={handleChange}
                  required
                />
              </Form.Group>
            </Col>
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>Model</Form.Label>
                <Form.Control
                  type="text"
                  name="model"
                  value={formData.model}
                  onChange={handleChange}
                />
              </Form.Group>
            </Col>
          </Row>

          <Row>
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>Manufacturer</Form.Label>
                <Form.Control
                  type="text"
                  name="manufacturer"
                  value={formData.manufacturer}
                  onChange={handleChange}
                />
              </Form.Group>
            </Col>
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>Starship Class</Form.Label>
                <Form.Control
                  type="text"
                  name="starshipClass"
                  value={formData.starshipClass}
                  onChange={handleChange}
                />
              </Form.Group>
            </Col>
          </Row>

          <Row>
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>Cost in Credits</Form.Label>
                <Form.Control
                  type="text"
                  name="costInCredits"
                  value={formData.costInCredits}
                  onChange={handleChange}
                />
              </Form.Group>
            </Col>
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>Length</Form.Label>
                <Form.Control
                  type="text"
                  name="length"
                  value={formData.length}
                  onChange={handleChange}
                />
              </Form.Group>
            </Col>
          </Row>

          <Row>
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>Max Atmosphering Speed</Form.Label>
                <Form.Control
                  type="text"
                  name="maxAtmospheringSpeed"
                  value={formData.maxAtmospheringSpeed}
                  onChange={handleChange}
                />
              </Form.Group>
            </Col>
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>Crew</Form.Label>
                <Form.Control
                  type="text"
                  name="crew"
                  value={formData.crew}
                  onChange={handleChange}
                />
              </Form.Group>
            </Col>
          </Row>

          <Row>
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>Passengers</Form.Label>
                <Form.Control
                  type="text"
                  name="passengers"
                  value={formData.passengers}
                  onChange={handleChange}
                />
              </Form.Group>
            </Col>
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>Cargo Capacity</Form.Label>
                <Form.Control
                  type="text"
                  name="cargoCapacity"
                  value={formData.cargoCapacity}
                  onChange={handleChange}
                />
              </Form.Group>
            </Col>
          </Row>

          <Row>
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>Consumables</Form.Label>
                <Form.Control
                  type="text"
                  name="consumables"
                  value={formData.consumables}
                  onChange={handleChange}
                />
              </Form.Group>
            </Col>
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>Hyperdrive Rating</Form.Label>
                <Form.Control
                  type="text"
                  name="hyperdriveRating"
                  value={formData.hyperdriveRating}
                  onChange={handleChange}
                />
              </Form.Group>
            </Col>
          </Row>

          <Row>
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>MGLT</Form.Label>
                <Form.Control
                  type="text"
                  name="mglt"
                  value={formData.mglt}
                  onChange={handleChange}
                />
              </Form.Group>
            </Col>
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>URL</Form.Label>
                <Form.Control
                  type="text"
                  name="url"
                  value={formData.url}
                  onChange={handleChange}
                />
              </Form.Group>
            </Col>
          </Row>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={onHide}>
            Cancel
          </Button>
          <Button variant="primary" type="submit">
            {starship ? 'Update' : 'Create'}
          </Button>
        </Modal.Footer>
      </Form>
    </Modal>
  );
};

export default StarshipModal;
