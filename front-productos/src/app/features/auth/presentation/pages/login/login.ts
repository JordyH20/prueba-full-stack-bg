import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { IniciarSesionUseCase } from '../../../application/use-cases/iniciar-sesion.use-case';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class LoginComponent {
  private readonly useCase = inject(IniciarSesionUseCase);
  private readonly fb = inject(FormBuilder);

  readonly cargando = signal(false);
  readonly error = signal<string | null>(null);

  readonly form = this.fb.group({
    correo: ['', [Validators.required, Validators.email, Validators.maxLength(254)]],
    contrasena: ['', [Validators.required, Validators.minLength(8)]]
  });

  enviar(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.cargando.set(true);
    this.error.set(null);

    const { correo, contrasena } = this.form.getRawValue();
    this.useCase.ejecutar(correo!, contrasena!).subscribe({
      error: () => {
        this.error.set('Correo o contraseña incorrectos.');
        this.cargando.set(false);
      }
    });
  }
}
