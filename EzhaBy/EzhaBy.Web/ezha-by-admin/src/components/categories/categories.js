import React, { Component } from 'react';
import CateringFacilitiesService from '../../services/catering-facilities-service';
import AddCategoryForm from './add-category';
import CategoriesList from './categories-list';
import Emitter from '../../services/event-emitter';
import { Form } from 'react-bootstrap';
import CategoriesService, {
  CATEGORIES_LIST_UPDATED
} from '../../services/categories-service';

export default class CategoriesPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      cateringFacilities: [],
      categories: [],
      cateringFacilityId: '-1'
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

  getCategories(id) {
    if (id && id !== '-1') {
      CategoriesService.getCategories(id).then(res => {
        this.setState({ categories: res.data });
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
    this.getCategories(this.state.cateringFacilityId);
    Emitter.on(CATEGORIES_LIST_UPDATED, _ =>
      this.getCategories(this.state.cateringFacilityId)
    );
  }

  componentWillUnmount() {
    Emitter.off(CATEGORIES_LIST_UPDATED);
  }

  renderCategoriesForm() {
    let id = this.state.cateringFacilityId;

    if (id && id !== '-1') {
      return (
        <div>
          <AddCategoryForm cateringFacilityId={this.state.cateringFacilityId} />
          <br />
          <CategoriesList cateringFacilityId={this.state.cateringFacilityId} categories={this.state.categories} />
        </div>
      );
    }
    if (this.state.cateringFacilities.length === 0) {
      return <div>Заведения отсутствуют</div>;
    }
    return <div>Заведение не выбрано</div>;
  }

  render() {
    return (
      <div>
        <Form.Group>
          <Form.Label>Заведение</Form.Label>
          <Form.Control
            as="select"
            disabled={this.state.cateringFacilities.length === 0}
            onChange={e => {
              this.setState({ cateringFacilityId: e.target.value });
              this.getCategories(e.target.value);
            }}
          >
            <option key={-1} value={-1}>
              Выберите заведение
            </option>
            {this.renderCateringFacilityOptions()}
          </Form.Control>
        </Form.Group>
        {this.renderCategoriesForm()}
      </div>
    );
  }
}
