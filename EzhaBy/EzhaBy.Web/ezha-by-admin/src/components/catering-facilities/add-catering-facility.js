import React, { Component } from "react";
import { Button, Form, Col, Row } from "react-bootstrap";
import { Redirect } from "react-router-dom";
import { LinkContainer } from "react-router-bootstrap";

import CateringFacilityForm from "./catering-facility-form";
import CateringFacilitiesService from "../../services/catering-facilities-service";
import TagsService from "../../services/tags-service";

export default class AddCateringFacility extends Component {
  constructor(props) {
    super(props);

    this.state = {
      tags: [],
      needRedirect: false
    };

    this.editorForm = React.createRef();
    this.onCateringFacilitySubmit = this.onCateringFacilitySubmit.bind(this);
  }

  async componentDidMount() {
    const tags = await TagsService.getTags();

    this.setState({
      tags: [...tags.data],
      needRedirect: false
    });
  }

  async onCateringFacilitySubmit() {
    const {
      nameInput,
      deliveryTimeInput,
      deliveryPriceInput,
      typeInput,
      workingHoursInput,
      townInput,
      streetInput,
      houseInput,
      state
    } = this.editorForm.current;

    const name = nameInput.current.value;
    const deliveryTime = deliveryTimeInput.current.value;
    const deliveryPrice = deliveryPriceInput.current.value;
    const type = typeInput.current.type.current.value;
    const workingHours = workingHoursInput.current.value;
    const town = townInput.current.town.current.value;
    const street = streetInput.current.value;
    const house = houseInput.current.value;
    const tagIds = state.cateringFacilityTags.map(tag => tag.id);

    await CateringFacilitiesService.createCateringFacility(
      name,
      deliveryTime,
      deliveryPrice,
      type,
      workingHours,
      town,
      street,
      house,
      tagIds
    );

    this.setState({
      needRedirect: true
    });
  }

  render() {
    const { tags, needRedirect } = this.state;

    const cateringFacilitiesRootPath = "/catering-facilities";

    const redirectElement = <Redirect to={cateringFacilitiesRootPath} />;

    const formElement = (
      <React.Fragment>
        <br />
        <LinkContainer to={cateringFacilitiesRootPath}>
          <Button>Назад</Button>
        </LinkContainer>
        <br />
        <Form>
          <CateringFacilityForm
            cateringFacilityTags={[]}
            tags={tags}
            ref={this.editorForm}
          />
          <Row>
            <Col sm="4">
              <Button onClick={this.onCateringFacilitySubmit}>Создать</Button>
            </Col>
          </Row>
        </Form>
      </React.Fragment>
    );

    return needRedirect ? redirectElement : formElement;
  }
}
