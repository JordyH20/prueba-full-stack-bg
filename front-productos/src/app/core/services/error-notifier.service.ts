import { Injectable, signal } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ErrorNotifierService {
  readonly mensaje = signal<string | null>(null);

  mostrar(texto: string): void {
    this.mensaje.set(texto);
    setTimeout(() => this.mensaje.set(null), 5000);
  }

  limpiar(): void {
    this.mensaje.set(null);
  }
}
