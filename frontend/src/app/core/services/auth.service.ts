import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginRequest, RegisterRequest, AuthResponse, UserMe } from '../../models/auth.models';
import { tap } from 'rxjs';
import { environment } from '../../../enviroment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private http = inject(HttpClient);
  private api = environment.apiUrl;

  login(data: LoginRequest) {
    return this.http.post<AuthResponse>(
        `${this.api}/auth/login`,
        data
      )
      .pipe(
        tap(res =>
          localStorage.setItem('token',res.token)
        )
      );
  }

  register(data: RegisterRequest) {
    return this.http
      .post<AuthResponse>(
        `${this.api}/auth/register`,
        data
      )
      .pipe(
        tap(res =>
          localStorage.setItem(
            'token',
            res.token
          )
        )
      );
  }

  getMe() {
    return this.http.get<UserMe>(
      `${this.api}/auth/me`
    );
  }

  getToken() {
    return localStorage.getItem('token');
  }

  isAuthenticated() {
    return !!this.getToken();
  }

  logout() {
    localStorage.removeItem('token');
  }
}
