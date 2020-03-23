import React, { Component } from 'react';
import { Button, Form, Col, Row } from 'react-bootstrap';
import TagsService from '../../services/tags-service';
import CategoriesService from '../../services/categories-service';

export default class AddCategoryForm extends Component {
  constructor(props) {
    super(props);

    this.categoryNameInput = React.createRef();
    this.onCategoryAddHandler = this.onCategoryAdd.bind(this);
  }

  onCategoryAdd() {
    CategoriesService.createCategory(this.props.cateringFacilityId, this.categoryNameInput.current.value);
    this.categoryNameInput.current.value = null;
  }

  render() {
    return (
      <div>
        <br />
        <Form>
          <Form.Label>Новая категория</Form.Label>
          <Form.Group as={Row}>
            <Col sm="3">
              <Form.Control
                ref={this.categoryNameInput}
                type="text"
                placeholder="Название категории"
              />
            </Col>
            <Button onClick={this.onCategoryAddHandler}>Добавить</Button>
          </Form.Group>
        </Form>
      </div>
    );
  }
}
