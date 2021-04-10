import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Order } from '../models/order';
import { ConfigService } from './config.service';

@Injectable()
export class OrdersService {
  constructor(private http: HttpClient) {}

  GetCafeOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(
      ConfigService.addBaseAddress('api/orders/cafe-orders')
    );
  }

  SetOrderStatus(id: string, status: number): Observable<Object> {
    const data = {
      orderStatus: status
    };
    return this.http.put(
      ConfigService.addBaseAddress(`api/orders/${id}/status`), data
    );
  }

  SetOrderCourier(id: string): Observable<Object> {
    return this.http.put(
      ConfigService.addBaseAddress(`api/orders/${id}/courier`), {}
    );
  }
}
