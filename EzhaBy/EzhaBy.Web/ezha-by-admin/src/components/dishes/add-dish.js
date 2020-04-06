import React, { Component } from "react";
import { Button, Form, Col, Row } from "react-bootstrap";
import { Redirect } from "react-router-dom";
import { LinkContainer } from "react-router-bootstrap";

import DishesService from "../../services/dishes-service";
import DishForm from "./dish-form";
import CategoriesService from "../../services/categories-service";

export default class AddDish extends Component {
  constructor(props) {
    super(props);

    this.state = {
      categories: [],
      needRedirect: false,
    };

    this.formResults = React.createRef();
    this.onDishSubmit = this.onDishSubmit.bind(this);
  }

  async onDishSubmit(event) {
    event.preventDefault();

    const {
      nameInput,
      descritionInput,
      priceInput,
      categoryInput,
    } = this.formResults.current;
    const name = nameInput.current.value;
    const description = descritionInput.current.value;
    const price = priceInput.current.value;
    const categoryId = categoryInput.current.value;

    await DishesService.createDish(name, description, price, categoryId);

    this.setState({
      needRedirect: true,
    });
  }

  componentDidMount() {
    const { cateringFacilityId } = this.props.match.params;

    CategoriesService.getCategories(cateringFacilityId).then((res) => {
      this.setState({
        categories: res.data,
        needRedirect: false,
      });
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
        <Form>
          <DishForm
            categories={categories}
            ref={this.formResults}
            categoryId={-1}
          />
          <Row>
            <Col sm="4">
              <Button onClick={this.onDishSubmit}>Создать</Button>
            </Col>
          </Row>
        </Form>
      </React.Fragment>
    );

    return needRedirect ? redirectElement : formElement;
  }
}
