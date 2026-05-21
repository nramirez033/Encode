import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatTabsModule } from '@angular/material/tabs';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-auth-page',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatTabsModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl:
    './auth-page.component.html'
})
export class AuthPageComponent {

  private fb = inject(FormBuilder);
  private auth = inject(AuthService);
  private router = inject(Router);

  loginForm = this.fb.nonNullable.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });

  registerForm = this.fb.nonNullable.group({
      nombre: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      equipo: ['', Validators.required]
    });

  login() {
    this.auth.login(this.loginForm.getRawValue())
    .subscribe(() => {
      this.router.navigate([
        '/tasks'
      ]);
    });
  }

  register() {
    this.auth.register(this.registerForm.getRawValue())
    .subscribe(() => {
      this.router.navigate([
        '/tasks'
      ]);
    });
  }
}
