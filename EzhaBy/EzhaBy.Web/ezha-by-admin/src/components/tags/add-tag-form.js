import React, { Component } from 'react';
import { Button, Form, Col, Row } from 'react-bootstrap';
import TagsService from '../../services/tags-service';

export default class AddTagForm extends Component {
  constructor(props) {
    super(props);

    this.tagNameInput = React.createRef();
    this.onTagAddHandler = this.onTagAdd.bind(this);
  }

  onTagAdd() {
    TagsService.createTag(this.tagNameInput.current.value);
    this.tagNameInput.current.value = null;
  }

  render() {
    return (
      <div>
        <br />
        <Form>
          <Form.Group as={Row} controlId="tagName">
            <Form.Label column sm="1">
              Новый тэг
            </Form.Label>
            <Col sm="4">
              <Form.Control ref={this.tagNameInput} type="text" placeholder="Название тэга" />
            </Col>
            <Button onClick={this.onTagAddHandler}>Добавить</Button>
          </Form.Group>
        </Form>
      </div>
    );
  }
}
