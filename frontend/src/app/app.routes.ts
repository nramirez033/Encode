import { Routes } from '@angular/router';

import {
  authGuard
} from './core/guards/auth.guard';

export const routes:
Routes = [
  {
    path: 'auth',
    loadComponent: () =>
      import('./pages/auth/auth-page.component').then(
        m => m.AuthPageComponent
      )
  },
  {
    path: 'tasks',
    canActivate: [
      authGuard
    ],
    loadComponent: () => import('./pages/tasks/tasks-page.component').then(
        m => m.TasksPageComponent
      )
  },
  {
    path: '**',
    redirectTo: 'tasks'
  }
];
