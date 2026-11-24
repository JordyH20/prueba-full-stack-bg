import { InjectionToken } from '@angular/core';
import { Observable } from 'rxjs';
import { Sesion } from '../models/sesion';

export interface AuthRepository {
  login(correo: string, contrasena: string): Observable<Sesion>;
}

export const AUTH_REPOSITORY = new InjectionToken<AuthRepository>('AUTH_REPOSITORY');
