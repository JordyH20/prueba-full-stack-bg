import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { AUTH_REPOSITORY } from '../../domain/ports/auth.repository';

@Injectable()
export class IniciarSesionUseCase {
  private readonly repo = inject(AUTH_REPOSITORY);
  private readonly router = inject(Router);

  ejecutar(correo: string, contrasena: string) {
    return this.repo.login(correo, contrasena).pipe(
      tap(sesion => {
        sessionStorage.setItem('token', sesion.token);
        sessionStorage.setItem('rol', sesion.rol);
        sessionStorage.setItem('nombre', sesion.nombre);
        this.router.navigate(['/productos']);
      })
    );
  }
}
