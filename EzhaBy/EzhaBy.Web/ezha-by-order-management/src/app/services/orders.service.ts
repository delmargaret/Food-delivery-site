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
}
