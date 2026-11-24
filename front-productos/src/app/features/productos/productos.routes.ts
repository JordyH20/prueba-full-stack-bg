import { Routes } from '@angular/router';
import { adminGuard } from '../../core/guards/admin.guard';
import { productosProviders } from './productos.providers';

export const productosRoutes: Routes = [
  {
    path: '',
    providers: productosProviders,
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./presentation/pages/productos-lista/productos-lista').then(m => m.ProductosListaComponent)
      },
      {
        path: 'nuevo',
        canActivate: [adminGuard],
        loadComponent: () =>
          import('./presentation/pages/producto-form/producto-form').then(m => m.ProductoFormComponent)
      },
      {
        path: ':id',
        canActivate: [adminGuard],
        loadComponent: () =>
          import('./presentation/pages/producto-form/producto-form').then(m => m.ProductoFormComponent)
      }
    ]
  }
];
