import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/models/order';
import { OrderStatuses } from 'src/app/models/orderStatuses';
import { PaymentTypes } from 'src/app/models/paymentTypes';
import { Towns } from 'src/app/models/towns';
import { OrdersService } from 'src/app/services/orders.service';

@Component({
  selector: 'app-cafe-page',
  templateUrl: './cafe-page.component.html',
  styleUrls: ['./cafe-page.component.css'],
})
export class CafePageComponent implements OnInit {
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

  constructor(private ordersService: OrdersService) {}

  ngOnInit(): void {
    this.getOrders();
    this.interval = setInterval(() => {
      this.getOrders();
    }, 60 * 2 * 1000);
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

  onChangeStatus(value: string) {
    console.log(value);
    this.selected = value;
  }

  getOrders() {
    this.ordersService.GetCafeOrders().subscribe(
      (orders: Order[]) => {
        this.allOrders = orders;
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
      () => (this.interval ? clearInterval(this.interval) : null)
    );
  }

  ngOnDestroy() {
    if (this.interval) {
      clearInterval(this.interval);
    }
  }
}
