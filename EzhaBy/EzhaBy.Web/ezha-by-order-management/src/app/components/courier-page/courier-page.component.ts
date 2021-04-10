import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/models/order';
import { OrderStatuses } from 'src/app/models/orderStatuses';
import { PaymentTypes } from 'src/app/models/paymentTypes';
import { Towns } from 'src/app/models/towns';
import { OrdersService } from 'src/app/services/orders.service';
import * as _ from 'lodash';
import { CourierDish } from 'src/app/models/courierDish';

@Component({
  selector: 'app-courier-page',
  templateUrl: './courier-page.component.html',
  styleUrls: ['./courier-page.component.css'],
})
export class CourierPageComponent implements OnInit {
  interval: any;
  allOrders: Order[] = [];
  orders: Order[] = [];
  towns = Towns;
  paymentTypes = PaymentTypes;
  statuses = OrderStatuses;
  dropdownStyles: any = {
    0: { 'background-color': 'rgb(176, 207, 167)' },
    1: { 'background-color': 'rgb(250, 207, 127)' },
    2: { 'background-color': 'rgb(161, 194, 255)' },
  };
  orderFilters: any = {
    0: 'active',
    1: 'closed',
    2: 'all',
  };
  selectedFilter: string = this.orderFilters[0];
  selected: string | null = null;
  isCourierButtonAvailable = true;

  constructor(private ordersService: OrdersService) {}

  ngOnInit(): void {
    this.getOrders();
    this.interval = setInterval(() => {
      this.getOrders();
    }, 60 * 3 * 1000);
  }

  getActive(event?: Event) {
    event ? event.preventDefault() : null;
    this.selectedFilter = this.orderFilters[0];
    this.orders = this.allOrders
      .filter((order) => order.orderStatus !== 3)
      .sort(
        (a, b) =>
          new Date(a.orderDateTime).valueOf() -
          new Date(b.orderDateTime).valueOf()
      );
  }

  getClosed(event?: Event) {
    event ? event.preventDefault() : null;
    this.selectedFilter = this.orderFilters[1];
    this.orders = this.allOrders
      .filter((order) => order.orderStatus === 3)
      .sort(
        (a, b) =>
          new Date(a.orderDateTime).valueOf() -
          new Date(b.orderDateTime).valueOf()
      );
  }

  getAll(event?: Event) {
    event ? event.preventDefault() : null;
    this.selectedFilter = this.orderFilters[2];
    this.orders = this.allOrders.sort(
      (a, b) =>
        new Date(a.orderDateTime).valueOf() -
        new Date(b.orderDateTime).valueOf()
    );
  }

  acceptOrder(event: Event, orderId: string) {
    event.preventDefault();
    event.stopPropagation();
    this.ordersService.AcceptOrder(orderId).subscribe(
      () => this.getOrders(),
      () => this.getOrders()
    );
  }

  rejectOrder(event: Event, orderId: string) {
    event.preventDefault();
    event.stopPropagation();
    this.ordersService.RejectOrder(orderId).subscribe(
      () => this.getOrders(),
      () => this.getOrders()
    );
  }

  onChangeStatus(event: Event, orderId: string) {
    event.preventDefault();
    event.stopPropagation();
    this.ordersService.SetOrderStatus(orderId, this.statuses.Done).subscribe(
      () => this.getOrders(),
      () => {}
    );
  }

  getOrders() {
    this.ordersService.GetCourierOrders().subscribe(
      (orders: Order[]) => {
        this.allOrders = orders;
        this.allOrders.forEach((order) => {
          order.courierDishes = _.chain(order.orderDishes)
            .groupBy((x) => x.cateringFacilityName)
            .toPairs()
            .map(function (currentData) {
              return _.zipObject(['cafeName', 'dishes'], currentData);
            })
            .map<CourierDish>(function (item) {
              return {
                cafeName: item.cafeName,
                dishes: item.dishes,
              } as CourierDish;
            })
            .value();
        });
        // var a = Object.assign({}, this.allOrders[0]);
        // a.orderStatus = 3;
        // a.isOrderAccepted = true;
        // a.orderDateTime = Date.now().toString();
        // this.allOrders.push(a);
        // this.allOrders.push(Object.assign({}, this.allOrders[0]));
        switch (this.selectedFilter) {
          case this.orderFilters[0]:
            this.getActive();
            break;
          case this.orderFilters[1]:
            this.getClosed();
            break;
          case this.orderFilters[2]:
            this.getAll();
            break;
        }
      },
      () => {
        this.interval ? clearInterval(this.interval) : null;
        this.allOrders = [];
      }
    );
  }

  ngOnDestroy() {
    if (this.interval) {
      clearInterval(this.interval);
    }
  }
}
