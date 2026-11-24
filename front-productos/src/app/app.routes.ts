import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { authProviders } from './features/auth/auth.providers';

export const routes: Routes = [
  { path: '', redirectTo: 'productos', pathMatch: 'full' },
  {
    path: 'login',
    providers: authProviders,
    loadComponent: () =>
      import('./features/auth/presentation/pages/login/login').then(m => m.LoginComponent)
  },
  {
    path: '',
    canActivate: [authGuard],
    providers: authProviders,
    loadComponent: () =>
      import('./shared/components/layout/layout').then(m => m.LayoutComponent),
    children: [
      {
        path: 'productos',
        loadChildren: () =>
          import('./features/productos/productos.routes').then(m => m.productosRoutes)
      }
    ]
  },
  { path: '**', redirectTo: 'productos' }
];
