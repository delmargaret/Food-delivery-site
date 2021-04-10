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
  orders: Order[] = [];
  towns = Towns;
  paymentTypes = PaymentTypes;
  statuses = OrderStatuses;
  dropdownStyles: any = {
    0: {'background-color': "rgba(125, 235, 95, 0.35)"},
    1: {'background-color': "rgba(255, 145, 0, 0.5)"},
    2: {'background-color': "rgba(0, 45, 255, 0.5)"},
    3: {'background-color': "rgba(235, 95, 95, 0.25)"}
  }
  selected: string | null = null;

  constructor(private ordersService: OrdersService) {}

  ngOnInit(): void {
    this.getOrders();
    this.interval = setInterval(() => {
      this.getOrders();
    }, 60 * 2 * 1000);
  }

  getActive(event: Event) {
    event.preventDefault();
  }

  getClosed(event: Event) {
    event.preventDefault();
  }

  getAll(event: Event) {
    event.preventDefault();
  }

  onChangeStatus(value: string) {
    console.log(value);
    this.selected = value;
  }

  getOrders() {
    this.ordersService
      .GetCafeOrders()
      .subscribe((orders: Order[]) => (this.orders = orders));
  }

  ngOnDestroy() {
    if (this.interval) {
      clearInterval(this.interval);
    }
  }
}
