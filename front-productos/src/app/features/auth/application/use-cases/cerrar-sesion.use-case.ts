import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable()
export class CerrarSesionUseCase {
  private readonly router = inject(Router);

  ejecutar(): void {
    sessionStorage.clear();
    this.router.navigate(['/login']);
  }
}
