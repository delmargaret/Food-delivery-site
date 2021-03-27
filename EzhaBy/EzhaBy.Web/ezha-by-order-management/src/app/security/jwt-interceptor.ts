import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const token = this.authService.getUser().token;
    req.headers.set("Content-Type", "application/json");
    req.headers.set('Authorization', 'Bearer ' + token);
    if (token) {
      const authReq = req.clone({
        headers: req.headers,
      });
      return next.handle(authReq);
    }
    return next.handle(req);
  }
}
