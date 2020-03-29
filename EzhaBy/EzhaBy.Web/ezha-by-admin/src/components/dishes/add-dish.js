import React, { Component } from "react";
import { Button, Form, Col, Row } from "react-bootstrap";
import DishesService from "../../services/dishes-service";
import DishForm from "./dish-form";
import CategoriesService from "../../services/categories-service";

export default class AddDish extends Component {
  constructor(props) {
    super(props);

    this.state = {
        categories: [],
      };
    this.formResults = React.createRef();
    this.onDishSubmit = this.onDishSubmit.bind(this);
  }

  componentDidMount(){
      const cateringFacilityId = this.props.match.params.cateringFacilityId;
      CategoriesService.getCategories(cateringFacilityId).then(res => {
        this.setState({ categories: res.data });
      });
  }

  onDishSubmit() {
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

    DishesService.createDish(name, description, price, categoryId);
  }

  render() {
    return (
      <div>
        <br />
        <Button href="/">Назад</Button>
        <br />
        <Form>
          <DishForm
            categories={this.state.categories}
            ref={this.formResults}
          />
          <Row>
            <Col sm="4">
              <Button onClick={this.onDishSubmit}>Создать</Button>
            </Col>
          </Row>
        </Form>
      </div>
    );
  }
}
