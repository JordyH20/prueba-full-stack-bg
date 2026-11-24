import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { ErrorNotifierService } from '../services/error-notifier.service';
import { LoggerService } from '../services/logger.service';

const MENSAJES: Record<number, string> = {
  400: 'Los datos enviados no son válidos.',
  401: 'Sesión expirada. Por favor iniciá sesión nuevamente.',
  403: 'No tenés permiso para realizar esta acción.',
  404: 'El recurso solicitado no existe.',
  409: 'Ya existe un registro con esos datos.',
  500: 'Error interno del servidor. Intentá más tarde.'
};

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const token = sessionStorage.getItem('token');
  const router = inject(Router);
  const notifier = inject(ErrorNotifierService);
  const logger = inject(LoggerService);

  const reqConToken = token
    ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
    : req;

  return next(reqConToken).pipe(
    catchError(err => {
      const status: number = err.status ?? 0;
      const mensaje = MENSAJES[status] ?? 'Sin conexión con el servidor.';

      logger.error(`HTTP ${status} en ${req.url}`, err);
      notifier.mostrar(mensaje);

      if (status === 401) {
        sessionStorage.clear();
        router.navigate(['/login']);
      }

      return throwError(() => err);
    })
  );
};
