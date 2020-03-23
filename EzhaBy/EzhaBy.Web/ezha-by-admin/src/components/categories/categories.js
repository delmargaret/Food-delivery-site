import React, { Component } from 'react';
import CateringFacilitiesService from '../../services/catering-facilities-service';
import AddCategoryForm from './add-category';
import CategoriesList from './categories-list'
import Emitter from '../../services/event-emitter';
import { Form } from 'react-bootstrap';
import CategoriesService, { CATEGORIES_LIST_UPDATED } from '../../services/categories-service';

export default class CategoriesPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      cateringFacilities: [],
      categories: []
    };

    this.cateringFacilityId = React.createRef();
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
    CategoriesService.getCategories(id).then(res =>
      this.setState({ categories: res.data })
    );
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
    console.log(JSON.stringify(this.cateringFacilityId.current.value) );
    this.getCategories(this.cateringFacilityId.current.value);
    Emitter.on(CATEGORIES_LIST_UPDATED, _ => this.getCategories(this.cateringFacilityId.current.value));
  }

  componentWillUnmount() {
    Emitter.off(CATEGORIES_LIST_UPDATED);
  }

  renderCategoriesForm() {
    return this.state.cateringFacilities.length > 0 ? (
      <div>
        <AddCategoryForm cateringFacilityId={this.cateringFacilityId.current.value}/>
        <br />
        <CategoriesList categories={this.state.categories}/>
      </div>
    ) : (
      <div>Заведения отсутствуют</div>
    );
  }

  render() {
    return (
      <div>
        <Form.Group>
          <Form.Label>Заведение</Form.Label>
          <Form.Control
            as="select"
            ref={this.cateringFacilityId}
            onChange={this.onCateringFacilitySelect}
          >
            {this.renderCateringFacilityOptions()}
          </Form.Control>
        </Form.Group>
        {this.renderCategoriesForm()}

        {/* <AddTagForm />
        <div id='tags-list'>
          <TagsList tags={this.state.tags} />
        </div> */}
      </div>
    );
  }
}
