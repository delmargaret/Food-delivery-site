import React, { Component } from "react";
import { Form, Col, Row } from "react-bootstrap";

export default class DishForm extends Component {
  constructor(props) {
    super(props);

    this.state = {
        categoryId: '-1'
    };

    this.nameInput = React.createRef();
    this.descritionInput = React.createRef();
    this.priceInput = React.createRef();
    this.categoryInput = React.createRef();
  }

  renderCategoriesOptions() {
    return this.props.categories.map(it => (
      <option key={it.id} value={it.id}>
        {it.categoryName}
      </option>
    ));
  }

  render() {
    return (
      <div>
        <Row>
          <Col sm="4">
            <Form.Group>
              <Form.Label>Название блюда</Form.Label>
              <Form.Control ref={this.nameInput} type="text" />
            </Form.Group>
          </Col>
        </Row>
        <Row>
          <Col sm="4">
            <Form.Group>
              <Form.Label>Описание</Form.Label>
              <Form.Control ref={this.descritionInput} type="text" />
            </Form.Group>
          </Col>
        </Row>
        <Row>
          <Col sm="4">
            <Form.Group>
              <Form.Label>Цена</Form.Label>
              <Form.Control ref={this.priceInput} type="text" />
            </Form.Group>
          </Col>
        </Row>
        <Row>
          <Col sm="4">
            <Form.Group>
              <Form.Label>Категория</Form.Label>
              <Form.Control
                as="select"
                onChange={e => {
                  this.setState({ categoryId: e.target.value });
                }}
                ref={this.categoryInput}
              >
                <option key={-1} value={-1}>
                  Выберите категорию
                </option>
                {this.renderCategoriesOptions()}
              </Form.Control>
            </Form.Group>
          </Col>
        </Row>
        <br />
      </div>
    );
  }
}
