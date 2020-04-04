import React from "react";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Redirect
} from "react-router-dom";

import NavbarComponent from "./components/nav-bar/nav-bar";
import NavTabs from "./components/nav-tabs/nav-tabs";

import TagsPage from "./components/tags/tags";
import CateringFacilitiesRouter from "./components/catering-facilities/catering-facilities-router";
import CategoriesPage from "./components/categories/categories";
import DishesRouter from "./components/dishes/dishes-router";
import PartnerRequestsPage from "./components/requests/partner-requests";
import CourierRequestsPage from "./components/requests/courier-requests";

import "./App.css";

import "./../node_modules/bootstrap/dist/css/bootstrap.min.css";
import "./../node_modules/react-bootstrap-table2-toolkit/dist/react-bootstrap-table2-toolkit.min.css";
import "./../node_modules/react-bootstrap-table-next/dist/react-bootstrap-table2.min.css";

function App() {
  return (
    <Router>
      <div className="App">
        <NavbarComponent />
        <NavTabs activeKey="partners" />
        <Switch>
          <Route exact path="/">
            <Redirect to="/partners" />
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
          <Route path="/complaints">
            <div>Жалобы</div>
          </Route>
        </Switch>
      </div>
    </Router>
  );
}

export default App;
