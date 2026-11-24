import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CerrarSesionUseCase } from '../../../features/auth/application/use-cases/cerrar-sesion.use-case';
import { ErrorNotifierService } from '../../../core/services/error-notifier.service';

@Component({
  selector: 'app-layout',
  imports: [RouterOutlet],
  templateUrl: './layout.html',
  styleUrl: './layout.css'
})
export class LayoutComponent {
  private readonly cerrarSesion = inject(CerrarSesionUseCase);
  readonly notifier = inject(ErrorNotifierService);

  readonly nombre = sessionStorage.getItem('nombre') ?? '';
  readonly rol = sessionStorage.getItem('rol') ?? '';

  salir(): void {
    this.cerrarSesion.ejecutar();
  }
}
