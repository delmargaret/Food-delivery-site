import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import NavbarComponent from "./components/nav-bar/nav-bar";
import NavTabs from "./components/nav-tabs/nav-tabs";

import TagsPage from "./components/tags/tags";
import CateringFacilitiesRouter from "./components/catering-facilities/catering-facilities-router";
import CategoriesPage from "./components/categories/categories";

import "./App.css";
// import HomePage from "./components/home-page/home-page";

import "./../node_modules/bootstrap/dist/css/bootstrap.min.css";
import "./../node_modules/react-bootstrap-table2-toolkit/dist/react-bootstrap-table2-toolkit.min.css";
import "./../node_modules/react-bootstrap-table-next/dist/react-bootstrap-table2.min.css";

function App() {
  return (
    <Router>
      <div className="App">
        <NavbarComponent />
        {/* <HomePage /> */}
        <NavTabs activeKey="partners" />
        <Switch>
          <Route exact path="/">
            <div>Default Home</div>
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
            <div>Блюда</div>
          </Route>
          <Route path="/orders">
            <div>Заказы</div>
          </Route>
          <Route path="/partners">
            <div>Партнеры</div>
          </Route>
          <Route path="/couriers">
            <div>Курьеры</div>
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
