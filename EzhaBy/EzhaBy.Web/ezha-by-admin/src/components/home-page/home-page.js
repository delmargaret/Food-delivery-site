import React, { Component } from 'react';
import { Tabs, Tab } from 'react-bootstrap';
import TagsPage from '../tags/tags';
import CateringFacilityForm from '../catering-facilities/catering-facility-form';

export default class HomePage extends Component {
  render() {
    return (
      <Tabs defaultActiveKey="catering-facilities" id="uncontrolled-tab-example">
        <Tab eventKey="tags" title="Тэги">
          <TagsPage />
        </Tab>
        <Tab eventKey="catering-facilities" title="Заведения">
          <CateringFacilityForm />
        </Tab>
        <Tab eventKey="categories" title="Категории блюд">
          Categories
        </Tab>
        <Tab eventKey="dishes" title="Блюда">
          Dishes
        </Tab>
        <Tab eventKey="partners" title="Партнеры">
          Partners
        </Tab>
        <Tab eventKey="couriers" title="Курьеры">
          Couriers
        </Tab>
        <Tab eventKey="orders" title="Заказы">
          Orders
        </Tab>
        <Tab eventKey="complaints" title="Жалобы">
          Complaints
        </Tab>
      </Tabs>
    );
  }
}
