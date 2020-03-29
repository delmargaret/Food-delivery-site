import React, { Component } from "react";
import { Button, Form, Col, Row } from "react-bootstrap";
import DishesService from "../../services/dishes-service";
import DishForm from "./dish-form";
import CategoriesService from "../../services/categories-service";

export default class UpdateDish extends Component {
  constructor(props) {
    super(props);
    this.state = {
      categories: []
    };

    this.formResults = React.createRef();
    this.onDishUpdate = this.onDishUpdate.bind(this);
  }

  componentDidMount() {
    const cateringFacilityId = this.props.match.params.cateringFacilityId;
    CategoriesService.getCategories(cateringFacilityId).then(res => {
      this.setState({ categories: res.data });
    });
    DishesService.getDish(this.props.match.params.id).then(result => {
      const {
        nameInput,
        descritionInput,
        priceInput,
        categoryInput
      } = this.formResults.current;

      nameInput.current.value = result.data.dishName;
      descritionInput.current.value = result.data.description;
      priceInput.current.value = result.data.price;
      categoryInput.current.value = result.data.cateringFacilityCategory.id;
    });
  }

  onDishUpdate() {
    const {
      nameInput,
      descritionInput,
      priceInput,
      state
    } = this.formResults.current;
    const name = nameInput.current.value;
    const description = descritionInput.current.value;
    const price = priceInput.current.value;
    const categoryId = state.categoryId;

    DishesService.updateDish(
      this.props.match.params.id,
      name,
      description,
      price,
      categoryId
    );
  }

  render() {
    return (
      <div>
        <br />
        <Button href="/">Назад</Button>
        <br />
        <Form onSubmit={this.onDishUpdate}>
          <DishForm categories={this.state.categories} ref={this.formResults} />
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
