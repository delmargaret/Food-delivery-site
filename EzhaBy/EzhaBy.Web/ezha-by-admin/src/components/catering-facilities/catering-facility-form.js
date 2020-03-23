import React, { Component } from "react";
import { Button, Form, Col, Row } from "react-bootstrap";
import TagsService from "../../services/tags-service";
import Towns from "../towns";
import CateringFacilityTypes from "../catering-facility-types";
import "./catering-facilities.css";

export default class CateringFacilityForm extends Component {
  constructor(props) {
    super(props);

    this.state = {
      cateringFacilityTags: [...props.tags]
    };

    this.tags = [];

    this.nameInput = React.createRef();
    this.typeInput = React.createRef();
    this.workingHoursInput = React.createRef();
    this.deliveryPriceInput = React.createRef();
    this.deliveryTimeInput = React.createRef();
    this.townInput = React.createRef();
    this.streetInput = React.createRef();
    this.houseInput = React.createRef();
    this.tagInput = React.createRef();

    this.onTagAdd = this.onTagAdd.bind(this);
    this.onTagDelete = this.onTagDelete.bind(this);

    this.shouldDisableTagSelector = this.shouldDisableTagSelector.bind(this);
  }

  componentDidMount() {
    TagsService.getTags().then(result => {
      this.tags = result.data;

      this.setState({
        cateringFacilityTags: [...this.props.tags]
      });
    });
  }

  shouldDisableTagSelector() {
    let tagsToShow = [...this.tags];

    if (
      this.state.cateringFacilityTags &&
      this.state.cateringFacilityTags.length > 0
    ) {
      tagsToShow = this.tags.filter(
        tag =>
          !this.state.cateringFacilityTags.some(cfTag => tag.id === cfTag.id)
      );
    }

    return tagsToShow.length === 0;
  }

  renderTagsOptions() {
    let tagsToShow = [...this.tags];

    if (
      this.state.cateringFacilityTags &&
      this.state.cateringFacilityTags.length > 0
    ) {
      tagsToShow = this.tags.filter(
        tag =>
          !this.state.cateringFacilityTags.some(cfTag => tag.id === cfTag.id)
      );
    }

    return tagsToShow.map(tag => {
      return (
        <option key={tag.id} value={tag.id}>
          {tag.tagName}
        </option>
      );
    });
  }

  renderTagsPreview() {
    if (
      this.state.cateringFacilityTags &&
      this.state.cateringFacilityTags.length > 0
    ) {
      return this.state.cateringFacilityTags.map(tag => (
        <div className="preview" key={tag.id}>
          {tag.tagName}
          <span className="close" onClick={() => this.onTagDelete(tag.id)}>
            x
          </span>
        </div>
      ));
    }
  }

  onTagDelete(tagId) {
    let cateringFacilityTags = [...this.state.cateringFacilityTags];

    cateringFacilityTags.splice(
      cateringFacilityTags.findIndex(tag => tag.id === tagId),
      1
    );

    this.setState({
      cateringFacilityTags: cateringFacilityTags
    });
  }

  onTagAdd() {
    const tagIdToAdd = this.tagInput.current.value;

    let cateringFacilityTags = [...this.state.cateringFacilityTags];

    cateringFacilityTags.push(this.tags.find(tag => tag.id === tagIdToAdd));

    this.setState({
      cateringFacilityTags: cateringFacilityTags
    });
  }

  render() {
    return (
      <div>
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
              <Form.Control
                as="select"
                disabled={this.shouldDisableTagSelector()}
                ref={this.tagInput}
              >
                {this.renderTagsOptions()}
              </Form.Control>
            </Col>
            <Col sm="1">
              <Button onClick={this.onTagAdd}>Добавить</Button>
            </Col>
          </Row>
        </Form.Group>
        <Col sm="4">
          <div ref={this.previewTag}>{this.renderTagsPreview()}</div>
        </Col>
        <br />
      </div>
    );
  }
}
