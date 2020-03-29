import React, { Component } from "react";
import CateringFacilitiesService from "../../services/catering-facilities-service";
import Emitter from "../../services/event-emitter";
import { Form, Button } from "react-bootstrap";
import DishesService, {
  DISHES_LIST_UPDATED
} from "../../services/dishes-service";
import CategoriesService from "../../services/categories-service";
import AddDish from "./add-dish";
import DishesList from "./dishes-list";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import UpdateDish from "./update-dish";

export default class DishesPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      cateringFacilities: [],
      categories: [],
      dishes: [],
      cateringFacilityId: "-1"
    };

    this.getCateringFacilities = this.getCateringFacilities.bind(this);
    this.getCategories = this.getCategories.bind(this);
  }

  getCateringFacilities() {
    CateringFacilitiesService.getCateringFacilities().then(result => {
      let cateringFacilities = result.data.map(res => {
        return { id: res.id, name: res.cateringFacilityName };
      });
      this.setState({ cateringFacilities: cateringFacilities });
    });
  }

  getCategories(cateringFacilityId) {
    if (cateringFacilityId && cateringFacilityId !== "-1") {
      CategoriesService.getCategories(cateringFacilityId).then(res => {
        this.setState({ categories: res.data });
      });
    }
  }

  getDishes(cateringFacilityId) {
    if (cateringFacilityId && cateringFacilityId !== "-1") {
      DishesService.getDishes(cateringFacilityId).then(res => {
        this.setState({ dishes: res.data });
      });
    }
  }

  renderCateringFacilityOptions() {
    return this.state.cateringFacilities.map(it => (
      <option key={it.id} value={it.id}>
        {it.name}
      </option>
    ));
  }

  componentDidMount() {
    this.getCateringFacilities();
    Emitter.on(DISHES_LIST_UPDATED, _ =>
      this.getDishes(this.state.cateringFacilityId)
    );
  }

  componentWillUnmount() {
    Emitter.off(DISHES_LIST_UPDATED);
  }

  renderDishesForm() {
    let id = this.state.cateringFacilityId;

    if (id && id !== "-1") {
      if (this.state.categories.length === 0) {
        return <div>Категории отсутствуют</div>;
      }
      return (
        <div>
          <br />
          <Button href={`/addDish/${id}`}>Добавить блюдо</Button>
          <br />
          <div id="dishes-list">
            <DishesList dishes={this.state.dishes} cateringFacilityId={id}/>
          </div>
        </div>
      );
    }
    if (this.state.cateringFacilities.length === 0) {
      return <div>Заведения отсутствуют</div>;
    }
    return <div>Заведение не выбрано</div>;
  }

  renderDishesPage() {
    return (
      <div>
        <Form.Group>
          <Form.Label>Заведение</Form.Label>
          <Form.Control
            as="select"
            disabled={this.state.cateringFacilities.length === 0}
            onChange={e => {
              this.setState({ cateringFacilityId: e.target.value });
              this.getDishes(e.target.value);
              this.getCategories(e.target.value);
            }}
          >
            <option key={-1} value={-1}>
              Выберите заведение
            </option>
            {this.renderCateringFacilityOptions()}
          </Form.Control>
        </Form.Group>
        {this.renderDishesForm()}
      </div>
    );
  }

  render() {
    return (
      <div>
        <Router>
          <Switch>
            <Route path="/addDish/:cateringFacilityId" component={AddDish}>
              {/* <AddDish categories={this.state.categories} /> */}
            </Route>
            <Route
              path="/updateDish/:id/:cateringFacilityId"
              component={UpdateDish}
            ></Route>
            <Route path="/">{this.renderDishesPage()}</Route>
          </Switch>
        </Router>
      </div>
    );
  }
}
