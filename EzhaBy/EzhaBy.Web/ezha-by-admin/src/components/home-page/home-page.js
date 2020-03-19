import React, { Component } from 'react';
import { Tabs, Tab } from 'react-bootstrap';
import TagsPage from '../tags/tags';

export default class HomePage extends Component {
  render() {
    return (
      <Tabs defaultActiveKey="tags" id="uncontrolled-tab-example">
        <Tab eventKey="tags" title="Тэги">
          <TagsPage />
        </Tab>
        <Tab eventKey="catering-facilities" title="Заведения">
          Catering Facilities
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
        <Tab eventKey="users" title="Пользователи">
          Users
        </Tab>
      </Tabs>
    );
  }
}
