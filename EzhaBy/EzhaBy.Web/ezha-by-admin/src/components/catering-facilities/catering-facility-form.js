import React, { Component } from 'react';
import { Button, Form, Col, Row } from 'react-bootstrap';
import TagsService from '../../services/tags-service';
import Towns from '../towns';
import CateringFacilityTypes from '../catering-facility-types';
import CateringFacilitiesService from '../../services/catering-facilities-service';

export default class CateringFacilityForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      tags: [],
      cateringFacilityTags: []
    };

    this.nameInput = React.createRef();
    this.typeInput = React.createRef();
    this.workingHoursInput = React.createRef();
    this.deliveryPriceInput = React.createRef();
    this.deliveryTimeInput = React.createRef();
    this.townInput = React.createRef();
    this.streetInput = React.createRef();
    this.houseInput = React.createRef();
    this.tagInput = React.createRef();

    this.onTagAddHandler = this.onTagAdd.bind(this);
    this.onCateringFacilitySubmit = this.onCateringFacilitySubmit.bind(this);
  }

  componentDidMount() {
    TagsService.getTags().then(result => {
      this.setState({ tags: result.data });
    });
  }

  renderTagsOptions() {
    return this.state.tags.map(tag => (
      <option key={tag.id} value={tag.id}>
        {tag.tagName}
      </option>
    ));
  }

  onCateringFacilitySubmit() {
    const name = this.nameInput.current.value;
    const deliveryTime = this.deliveryTimeInput.current.value;
    const deliveryPrice = this.deliveryPriceInput.current.value;
    const type = this.typeInput.current.value;
    const workingHours = this.workingHoursInput.current.value;
    const town = this.townInput.current.value;
    const street = this.streetInput.current.value;
    const house = this.houseInput.current.value;

    CateringFacilitiesService.createCateringFacility(
      name,
      deliveryTime,
      deliveryPrice,
      type,
      workingHours,
      town,
      street,
      house,
      this.state.cateringFacilityTags
    );
  }

  onTagAdd() {
    let tags = this.state.cateringFacilityTags;
    tags.push(this.tagInput.current.value);
    this.setState({ cateringFacilityTags: tags });
  }

  render() {
    return (
      <div>
        <br />
        <Form onSubmit={this.onCateringFacilitySubmit}>
          <Row>
            <Col sm="4">
              <Form.Group>
                <Form.Label>Название заведения</Form.Label>
                <Form.Control ref={this.nameInput} type="text" />
              </Form.Group>
            </Col>
          </Row>
          <Row>
            <Col sm="2">
              <CateringFacilityTypes ref={this.typeInput} />
            </Col>
            <Col sm="2">
              <Form.Group>
                <Form.Label>Время работы</Form.Label>
                <Form.Control ref={this.workingHoursInput} type="text" />
              </Form.Group>
            </Col>
          </Row>
          <Row>
            <Col sm="2">
              <Form.Group>
                <Form.Label>Стоимость доставки</Form.Label>
                <Form.Control ref={this.deliveryPriceInput} type="text" />
              </Form.Group>
            </Col>
            <Col sm="2">
              <Form.Group>
                <Form.Label>Время доставки</Form.Label>
                <Form.Control ref={this.deliveryTimeInput} type="text" />
              </Form.Group>
            </Col>
          </Row>
          <Row>
            <Col sm="4">
              <Towns ref={this.townInput} />
            </Col>
          </Row>
          <Row>
            <Col sm="2">
              <Form.Group>
                <Form.Label>Улица</Form.Label>
                <Form.Control ref={this.streetInput} type="text" />
              </Form.Group>
            </Col>
            <Col sm="2">
              <Form.Group>
                <Form.Label>Дом</Form.Label>
                <Form.Control ref={this.houseInput} type="text" />
              </Form.Group>
            </Col>
          </Row>
          <Form.Group>
            <Form.Label>Тэги</Form.Label>
            <Row>
              <Col sm="3">
                <Form.Control as="select" ref={this.tagInput}>
                  {this.renderTagsOptions()}
                </Form.Control>
              </Col>
              <Col sm="1">
                <Button onClick={this.onTagAddHandler}>Добавить</Button>
              </Col>
            </Row>
          </Form.Group>
          <Row>
            <Col sm="4">
              <Button type="submit">Создать</Button>
            </Col>
          </Row>
        </Form>
      </div>
    );
  }
}
