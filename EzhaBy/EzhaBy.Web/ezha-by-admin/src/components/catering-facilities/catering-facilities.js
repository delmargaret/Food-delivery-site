import React, { Component } from 'react';
import CateringFacilitiesList from './catering-facilities-list';
import CateringFacilitiesService, {
  CF_LIST_UPDATED
} from '../../services/catering-facilities-service';
import { Button } from 'react-bootstrap';
import Emitter from '../../services/event-emitter';
import AddCateringFacility from './add-catering-facility';
import UpdateCateringFacility from './update-catering-facility';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';

export default class CateringFacilitiesPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      cateringFacilities: []
    };

    this.getCateringFacilities = this.getCateringFacilities.bind(this);
  }

  getCateringFacilities() {
    CateringFacilitiesService.getCateringFacilities().then(result => {
      this.setState({ cateringFacilities: result.data });
    });
  }

  componentDidMount() {
    this.getCateringFacilities();
    Emitter.on(CF_LIST_UPDATED, _ => this.getCateringFacilities());
  }

  componentWillUnmount() {
    Emitter.off(CF_LIST_UPDATED);
  }

  render() {
    return (
      <Router>
        <Switch>
          <Route path="/add">
            <AddCateringFacility />
          </Route>
          <Route path="/update/:id" component={UpdateCateringFacility}></Route>
          <Route path="/">
            <div>
              <br />
              <Button href="/add">Добавить заведение</Button>
              <br />
              <div id="catering-facilities-list">
                <CateringFacilitiesList
                  cateringFacilities={this.state.cateringFacilities}
                />
              </div>
            </div>
          </Route>
        </Switch>
      </Router>
    );
  }
}
