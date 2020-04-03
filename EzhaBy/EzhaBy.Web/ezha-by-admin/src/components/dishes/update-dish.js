import React, { Component } from "react";
import { Button, Form, Col, Row } from "react-bootstrap";
import { Redirect } from "react-router-dom";
import { LinkContainer } from "react-router-bootstrap";

import DishesService from "../../services/dishes-service";
import DishForm from "./dish-form";
import CategoriesService from "../../services/categories-service";

export default class UpdateDish extends Component {
  constructor(props) {
    super(props);
    this.state = {
      categories: [],
      needRedirect: false
    };

    this.formResults = React.createRef();
    this.onDishUpdate = this.onDishUpdate.bind(this);
  }

  async componentDidMount() {
    const { cateringFacilityId, id } = this.props.match.params;

    const categoriesList = await CategoriesService.getCategories(
      cateringFacilityId
    );

    this.setState({
      categories: categoriesList.data,
      needRedirect: false
    });

    const dishDetails = await DishesService.getDish(id);

    const {
      nameInput,
      descritionInput,
      priceInput,
      categoryInput
    } = this.formResults.current;

    nameInput.current.value = dishDetails.data.dishName;
    descritionInput.current.value = dishDetails.data.description;
    priceInput.current.value = dishDetails.data.price;
    categoryInput.current.value = dishDetails.data.cateringFacilityCategory.id;
  }

  async onDishUpdate(event) {
    event.preventDefault();

    const { id } = this.props.match.params;

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

    console.log(name, description, price, categoryId);

    await DishesService.updateDish(id, name, description, price, categoryId);

    this.setState({
      needRedirect: true
    });
  }

  render() {
    const { categories, needRedirect } = this.state;

    const dishesRootPath = "/dishes";

    const redirectElement = <Redirect to={dishesRootPath} />;

    const formElement = (
      <React.Fragment>
        <br />
        <LinkContainer to="/dishes">
          <Button>Назад</Button>
        </LinkContainer>
        <br />
        <Form onSubmit={this.onDishUpdate}>
          <DishForm categories={categories} ref={this.formResults} />
          <Row>
            <Col sm="4">
              <Button type="submit">Изменить</Button>
            </Col>
          </Row>
        </Form>
      </React.Fragment>
    );

    return needRedirect ? redirectElement : formElement;
  }
}
