import React, { Component } from "react";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Redirect,
} from "react-router-dom";
import { Row, Col } from "react-bootstrap";

import NavbarComponent from "./components/nav-bar/nav-bar";
import NavTabs from "./components/nav-tabs/nav-tabs";

import TagsPage from "./components/tags/tags";
import CateringFacilitiesRouter from "./components/catering-facilities/catering-facilities-router";
import CategoriesPage from "./components/categories/categories";
import DishesRouter from "./components/dishes/dishes-router";
import PartnerRequestsPage from "./components/requests/partner-requests";
import CourierRequestsPage from "./components/requests/courier-requests";
import FeedbacksPage from "./components/feedbacks/feedbacks";

import "./App.css";

import "./../node_modules/bootstrap/dist/css/bootstrap.min.css";
import "./../node_modules/react-bootstrap-table2-toolkit/dist/react-bootstrap-table2-toolkit.min.css";
import "./../node_modules/react-bootstrap-table-next/dist/react-bootstrap-table2.min.css";

import leaves from "./leaves.png";
import FooterComponent from "./components/footer/footer";

export default class App extends Component {
  render() {
    return (
      <Router>
        <div className="App">
          <NavbarComponent />
          <NavTabs activeKey={window.location.pathname.slice(1) || "tags"} />
          <Row id="main-row">
            <Col>
              {" "}
              <img alt="" src={leaves} className="leaves-left" />
            </Col>
            <Col xs={8}>
              <Switch>
                <Route exact path="/">
                  <Redirect to="/tags" />
                </Route>
                <Route path="/tags">
                  <TagsPage />
                </Route>
                <Route path="/catering-facilities">
                  <CateringFacilitiesRouter />
                </Route>
                <Route path="/categories">
                  <CategoriesPage />
                </Route>
                <Route path="/dishes">
                  <DishesRouter />
                </Route>
                <Route path="/partners">
                  <PartnerRequestsPage />
                </Route>
                <Route path="/couriers">
                  <CourierRequestsPage />
                </Route>
                <Route path="/feedbacks">
                  <FeedbacksPage />
                </Route>
              </Switch>
            </Col>
            <Col>
              <img alt="" src={leaves} className="leaves-right" />
            </Col>
          </Row>
          <br />
          <FooterComponent />
        </div>
      </Router>
    );
  }
}
