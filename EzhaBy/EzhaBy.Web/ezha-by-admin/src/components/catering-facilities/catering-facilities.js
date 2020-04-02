import React, { Component } from "react";
import { Button } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";

import CateringFacilitiesList from "./catering-facilities-list";
import CateringFacilitiesService, {
  CF_LIST_UPDATED
} from "../../services/catering-facilities-service";
import Emitter from "../../services/event-emitter";

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
      <React.Fragment>
        <br />
        <LinkContainer to="/catering-facilities/new">
          <Button>Добавить заведение</Button>
        </LinkContainer>
        <br />
        <div id="catering-facilities-list">
          <CateringFacilitiesList
            cateringFacilities={this.state.cateringFacilities}
          />
        </div>
      </React.Fragment>
    );
  }
}
