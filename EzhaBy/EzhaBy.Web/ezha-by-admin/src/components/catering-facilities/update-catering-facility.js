import React, { Component } from "react";
import { Button, Form, Col, Row } from "react-bootstrap";
import CateringFacilityForm from "./catering-facility-form";
import CateringFacilitiesService from "../../services/catering-facilities-service";

export default class UpdateCateringFacility extends Component {
  constructor(props) {
    super(props);
    this.state = {
      tags: []
    };

    this.formResults = React.createRef();
    this.onCateringFacilityUpdate = this.onCateringFacilityUpdate.bind(this);
  }

  componentDidMount() {
    CateringFacilitiesService.getCateringFacility(
      this.props.match.params.id
    ).then(result => {
      const {
        nameInput,
        deliveryTimeInput,
        deliveryPriceInput,
        typeInput,
        workingHoursInput,
        townInput,
        streetInput,
        houseInput
      } = this.formResults.current;

      nameInput.current.value = result.data.cateringFacilityName;
      deliveryTimeInput.current.value = result.data.deliveryTime;
      deliveryPriceInput.current.value = result.data.deliveryPrice;
      typeInput.current.type.current.value = result.data.cateringFacilityType;
      workingHoursInput.current.value = result.data.workingHours;
      townInput.current.town.current.value = result.data.town;
      streetInput.current.value = result.data.street;
      houseInput.current.value = result.data.houseNumber;

      this.setState({ tags: result.data.cateringFacilityTags });
    });
  }

  onCateringFacilityUpdate() {
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
    } = this.formResults.current;
    const name = nameInput.current.value;
    const deliveryTime = deliveryTimeInput.current.value;
    const deliveryPrice = deliveryPriceInput.current.value;
    const type = typeInput.current.type.current.value;
    const workingHours = workingHoursInput.current.value;
    const town = townInput.current.town.current.value;
    const street = streetInput.current.value;
    const house = houseInput.current.value;
    const tagIds = state.cateringFacilityTags.map(tag => tag.id);

    CateringFacilitiesService.updateCateringFacility(
      this.props.match.params.id,
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
  }

  render() {
    return (
      <div>
        <br />
        <Button href="/">Назад</Button>
        <br />
        <Form onSubmit={this.onCateringFacilityUpdate}>
          <CateringFacilityForm tags={this.state.tags} ref={this.formResults} />
          <Row>
            <Col sm="4">
              <Button type="submit">Изменить</Button>
            </Col>
          </Row>
        </Form>
      </div>
    );
  }
}
