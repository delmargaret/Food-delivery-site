import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CourierRequest } from '../models/courierRequest';
import { PartnerRequest } from '../models/partnerRequest';
import { ConfigService } from './config.service';

@Injectable()
export class RequestsService {
  constructor(private http: HttpClient) {}

  AddPartnerRequest(data: PartnerRequest): Observable<Object> {
    return this.http.post(
      ConfigService.addBaseAddress(`api/requests/partners`),
      data
    );
  }

  AddCourierRequest(data: CourierRequest): Observable<Object> {
    return this.http.post(
      ConfigService.addBaseAddress(`api/requests/couriers`),
      data
    );
  }
}
