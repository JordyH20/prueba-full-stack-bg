import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const adminGuard: CanActivateFn = () => {
  const router = inject(Router);
  const rol = sessionStorage.getItem('rol');
  if (rol === 'Admin') return true;
  return router.createUrlTree(['/productos']);
};
