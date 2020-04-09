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
      validated: false,
    };

    this.formResults = React.createRef();
    this.onDishSubmit = this.onDishSubmit.bind(this);
  }

  async onDishSubmit(event) {
    event.preventDefault();
    event.stopPropagation();

    const form = event.currentTarget;
    let needRedirect = false;

    if (form.checkValidity()) {
      needRedirect = true;

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
    }

    this.setState({
      needRedirect: needRedirect,
      validated: true,
    });
  }

  componentDidMount() {
    const { cateringFacilityId } = this.props.match.params;

    CategoriesService.getCategories(cateringFacilityId).then((res) => {
      this.setState({
        categories: res.data,
        needRedirect: false,
        validated: false,
      });
    });
  }

  render() {
    const { categories, needRedirect, validated } = this.state;
    const { cateringFacilityId } = this.props.match.params;

    const dishesCateringFacilityPage = `/dishes/catering-facility/${cateringFacilityId}`;

    const redirectElement = <Redirect to={dishesCateringFacilityPage} />;

    const formElement = (
      <React.Fragment>
        <br />
        <LinkContainer to={dishesCateringFacilityPage} isActive={() => false}>
          <Button>Назад</Button>
        </LinkContainer>
        <br />
        <Form noValidate validated={validated} onSubmit={this.onDishSubmit}>
          <DishForm
            categories={categories}
            ref={this.formResults}
            categoryId={-1}
          />
          <Button type="submit" className="btn-red">
            Создать
          </Button>
        </Form>
      </React.Fragment>
    );

    return needRedirect ? redirectElement : formElement;
  }
}
