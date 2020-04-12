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
import LoginService from "./services/login-service";
import LoginPage from "./components/login-page/login-page";

export default class App extends Component {
  constructor(props) {
    super(props);
    this.state = { isAuthorized: false, role: null };

    this.logIn = this.logIn.bind(this);
    this.logOut = this.logOut.bind(this);
  }

  componentDidMount() {
    var user = LoginService.getUser();
    if (user) {
      if (user.token && !this.state.isAuthorized) {
        this.setState({
          isAuthorized: true,
          role: user.role,
        });
      } else if (!user.token && this.state.isAuthorized) {
        this.setState({ isAuthorized: false });
      }
    }
  }

  logOut() {
    this.setState({ isAuthorized: false });
    LoginService.removeUser();
  }

  logIn() {
    this.setState({ isAuthorized: true });
    return <Redirect to="/" />;
  }

  render() {
    const { isAuthorized } = this.state;
    //console.log(isAuthorized, this.state.role);
    //LoginService.removeUser()

    return (
      <Router>
        <div className="App">
          <NavbarComponent isAuthorized={isAuthorized} logOut={this.logOut} />
          <NavTabs activeKey={window.location.pathname.slice(1) || "tags"} />
          <Row id="main-row">
            <Col>
              {" "}
              <img alt="" src={leaves} className="leaves-left" />
            </Col>
            <Col xs={8}>
              <Switch>
                <Route exact path="/">
                  {isAuthorized ? (
                    <Redirect to="/tags" />
                  ) : (
                    <Redirect to="/login" />
                  )}
                </Route>
                <Route path="/tags">
                  {isAuthorized ? <TagsPage /> : <Redirect to="/login" />}
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
                <Route path="/login">
                  {isAuthorized ? (
                    <Redirect to="/" />
                  ) : (
                    <LoginPage logIn={this.logIn} />
                  )}
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
