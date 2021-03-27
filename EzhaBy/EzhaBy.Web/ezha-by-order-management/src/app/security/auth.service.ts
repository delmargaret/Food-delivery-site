import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Token } from '../models/token';
import { ConfigService } from '../services/config.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private tokenKey = 'token';
  private roleKey = 'role';
  private userId = 'userId';
  private tokenHelper = new JwtHelperService();

  constructor(private http: HttpClient, private router: Router) {}

  public hasValidToken(): boolean {
    const token = sessionStorage.getItem(this.tokenKey);
    if (token) {
      return !this.tokenHelper.isTokenExpired(token);
    }
    return false;
  }

  public getUser() {
    const token = sessionStorage.getItem(this.tokenKey);
    const role = sessionStorage.getItem(this.roleKey);
    const userId = sessionStorage.getItem(this.userId);

    return {
      token: token,
      role: role,
      userId: userId,
    };
  }

  public removeUser() {
    sessionStorage.removeItem(this.tokenKey);
    sessionStorage.removeItem(this.roleKey);
    sessionStorage.removeItem(this.userId);
  }

  public async Authorize(email: string, password: string, role: string) {
    const data = {
      email: email,
      password: password,
    };

    const res = this.http.post<Token>(ConfigService.addBaseAddress('api/token'), data)
        .subscribe((token) => {
            if (!token)
                return CREDENTIALS_NOT_FOUND;

            if (token.role !== role)
                return WRONG_ROLE;

            sessionStorage.setItem(this.tokenKey, token.token);
            sessionStorage.setItem(this.roleKey, token.role);
            sessionStorage.setItem(this.userId, token.userId);
            return CREDENTIALS_OK;
        });
  }
}

export const USER_LOGGED = 'USER_LOGGED';
export const USER_LOGGED_OUT = 'USER_LOGGED_OUT';

export const CREDENTIALS_NOT_FOUND = 'CREDENTIALS_NOT_FOUND';
export const WRONG_ROLE = 'WRONG_ROLE';
export const CREDENTIALS_OK = 'CREDENTIALS_OK';
export const CREDENTIALS_NOT_CHECKED = 'CREDENTIALS_NOT_CHECKED';
