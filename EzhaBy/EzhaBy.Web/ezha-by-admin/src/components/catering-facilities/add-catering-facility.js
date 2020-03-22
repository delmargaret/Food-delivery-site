import React, { Component } from 'react';
import { Button, Form, Col, Row } from 'react-bootstrap';
import CateringFacilityForm from './catering-facility-form';
import CateringFacilitiesService from '../../services/catering-facilities-service';
import { Redirect } from 'react-router-dom';

export default class AddCateringFacility extends Component {
  constructor(props) {
    super(props);

    this.formResults = React.createRef();
    this.onCateringFacilitySubmit = this.onCateringFacilitySubmit.bind(this);
  }

  onCateringFacilitySubmit() {
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

    CateringFacilitiesService.createCateringFacility(
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
        <Form >
          <CateringFacilityForm tags={[]} ref={this.formResults} />
          <Row>
            <Col sm="4">
              <Button onClick={this.onCateringFacilitySubmit} >Создать</Button>
            </Col>
          </Row>
        </Form>
      </div>
    );
  }
}
