import { Provider } from '@angular/core';
import { AUTH_REPOSITORY } from './domain/ports/auth.repository';
import { HttpAuthRepository } from './infrastructure/adapters/http-auth.repository';
import { IniciarSesionUseCase } from './application/use-cases/iniciar-sesion.use-case';
import { CerrarSesionUseCase } from './application/use-cases/cerrar-sesion.use-case';

export const authProviders: Provider[] = [
  { provide: AUTH_REPOSITORY, useClass: HttpAuthRepository },
  IniciarSesionUseCase,
  CerrarSesionUseCase
];
