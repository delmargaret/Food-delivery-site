import React, { Component } from 'react';
import { Form } from 'react-bootstrap';

const TOWNS = [
  {
    id: 0,
    name: 'Минск'
  },
  {
    id: 1,
    name: 'Гродно'
  },
  {
    id: 2,
    name: 'Гомель'
  },
  {
    id: 3,
    name: 'Брест'
  },
  {
    id: 4,
    name: 'Могилёв'
  },
  {
    id: 5,
    name: 'Витебск'
  }
];

export default class Towns extends Component {
  renderOptions() {
    return TOWNS.map(town => <option key={town.id} value={town.id}>{town.name}</option>);
  }

  render() {
    return (
      <div>
        <Form.Group>
          <Form.Label>Город</Form.Label>
          <Form.Control as="select">
            {this.renderOptions()}
          </Form.Control>
        </Form.Group>
      </div>
    );
  }
}
