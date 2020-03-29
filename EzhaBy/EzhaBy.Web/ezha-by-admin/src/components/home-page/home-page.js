import React, { Component } from "react";
import { Tabs, Tab } from "react-bootstrap";
import TagsPage from "../tags/tags";
import CateringFacilitiesPage from "../catering-facilities/catering-facilities";
import CategoriesPage from "../categories/categories";
import DishesPage from "../dishes/dishes";
import PartnerRequestsPage from "../partner-requests/partner-requests";

export default class HomePage extends Component {
  render() {
    return (
      <Tabs
        defaultActiveKey="partners"
        id="uncontrolled-tab-example"
      >
        <Tab eventKey="tags" title="Тэги">
          <TagsPage />
        </Tab>
        <Tab eventKey="catering-facilities" title="Заведения">
          <CateringFacilitiesPage />
        </Tab>
        <Tab eventKey="categories" title="Категории блюд">
          <CategoriesPage />
        </Tab>
        <Tab eventKey="dishes" title="Блюда">
          <DishesPage />
        </Tab>
        <Tab eventKey="orders" title="Заказы">
          Orders
        </Tab>
        <Tab eventKey="partners" title="Партнеры">
          <PartnerRequestsPage />
        </Tab>
        <Tab eventKey="couriers" title="Курьеры">
          Couriers
        </Tab>
        <Tab eventKey="complaints" title="Жалобы">
          Complaints
        </Tab>
      </Tabs>
    );
  }
}
