import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class LoggerService {
  error(mensaje: string, detalle?: unknown): void {
    console.error(`[ERROR] ${mensaje}`, detalle ?? '');
  }

  warn(mensaje: string, detalle?: unknown): void {
    console.warn(`[WARN] ${mensaje}`, detalle ?? '');
  }
}
