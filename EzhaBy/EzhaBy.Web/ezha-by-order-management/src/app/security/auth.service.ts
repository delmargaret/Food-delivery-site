import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Token } from '../models/token';
import { CredentialsStatus } from '../models/credentialsStatus';
import { ConfigService } from '../services/config.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private tokenKey = 'token';
  private roleKey = 'role';
  private userId = 'userId';
  private allowedRoles = ['Courier', 'CafeAdmin'];
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

  public authorize(email: string, password: string) {
    const data = {
      email: email,
      password: password,
    };

    return this.http.post<Token>(
      ConfigService.addBaseAddress('api/token'),
      data
    );
  }

  public saveUser(token: Token) {
    if (!token) return CredentialsStatus.CREDENTIALS_NOT_FOUND;

    if (!this.allowedRoles.includes(token.role))
      return CredentialsStatus.WRONG_ROLE;

    sessionStorage.setItem(this.tokenKey, token.token);
    sessionStorage.setItem(this.roleKey, token.role);
    sessionStorage.setItem(this.userId, token.userId);
    return CredentialsStatus.CREDENTIALS_OK;
  }
}
