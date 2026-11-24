import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { DecimalPipe } from '@angular/common';
import { ObtenerProductoUseCase } from '../../../application/use-cases/obtener-producto.use-case';
import { GuardarProductoUseCase } from '../../../application/use-cases/guardar-producto.use-case';
import { GuardarLoteUseCase } from '../../../application/use-cases/guardar-lote.use-case';
import { EliminarLoteUseCase } from '../../../application/use-cases/eliminar-lote.use-case';
import { Lote } from '../../../domain/models/lote';

@Component({
  selector: 'app-producto-form',
  imports: [ReactiveFormsModule, RouterLink, DecimalPipe],
  templateUrl: './producto-form.html',
  styleUrl: './producto-form.css'
})
export class ProductoFormComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly obtener = inject(ObtenerProductoUseCase);
  private readonly guardar = inject(GuardarProductoUseCase);
  private readonly guardarLote = inject(GuardarLoteUseCase);
  private readonly eliminarLote = inject(EliminarLoteUseCase);

  readonly productoId = signal<number | null>(null);
  readonly cargando = signal(false);
  readonly guardando = signal(false);
  readonly lotes = signal<Lote[]>([]);
  readonly loteEditando = signal<Lote | null>(null);
  readonly guardandoLote = signal(false);

  readonly form = this.fb.group({
    codigo: ['', [Validators.required, Validators.maxLength(50)]],
    nombre: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(150)]],
    descripcion: ['', Validators.maxLength(500)]
  });

  readonly loteForm = this.fb.group({
    numeroLote: ['', [Validators.required, Validators.maxLength(80)]],
    precio: [null as number | null, [Validators.required, Validators.min(0.01)]],
    stock: [null as number | null, [Validators.required, Validators.min(0)]]
  });

  get esNuevo() { return this.productoId() === null; }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      const id = Number(idParam);
      this.productoId.set(id);
      this.cargando.set(true);
      this.obtener.ejecutar(id).subscribe({
        next: p => {
          this.form.patchValue({ codigo: p.codigo, nombre: p.nombre, descripcion: p.descripcion });
          this.lotes.set(p.lotes);
          this.cargando.set(false);
        },
        error: () => { this.cargando.set(false); this.router.navigate(['/productos']); }
      });
    }
  }

  enviar(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.guardando.set(true);
    const v = this.form.getRawValue();
    const form = { codigo: v.codigo!, nombre: v.nombre!, descripcion: v.descripcion ?? '' };
    const obs = this.esNuevo
      ? this.guardar.crear(form)
      : this.guardar.actualizar(this.productoId()!, form);

    obs.subscribe({
      next: p => {
        if (this.esNuevo) this.router.navigate(['/productos', p.id]);
        else this.guardando.set(false);
      },
      error: () => this.guardando.set(false)
    });
  }

  editarLote(lote: Lote): void {
    this.loteEditando.set(lote);
    this.loteForm.patchValue({ numeroLote: lote.numeroLote, precio: lote.precio, stock: lote.stock });
  }

  cancelarLote(): void {
    this.loteEditando.set(null);
    this.loteForm.reset();
  }

  enviarLote(): void {
    if (this.loteForm.invalid) { this.loteForm.markAllAsTouched(); return; }
    const v = this.loteForm.getRawValue();
    const form = { numeroLote: v.numeroLote!, precio: v.precio!, stock: v.stock! };
    const id = this.productoId()!;
    this.guardandoLote.set(true);

    const obs = this.loteEditando()
      ? this.guardarLote.actualizar(id, this.loteEditando()!.id, form)
      : this.guardarLote.crear(id, form);

    obs.subscribe({
      next: lote => {
        const editando = this.loteEditando();
        if (editando) {
          this.lotes.set(this.lotes().map(l => l.id === lote.id ? lote : l));
        } else {
          this.lotes.set([lote, ...this.lotes()]);
        }
        this.loteEditando.set(null);
        this.loteForm.reset();
        this.guardandoLote.set(false);
      },
      error: () => this.guardandoLote.set(false)
    });
  }

  confirmarEliminarLote(lote: Lote): void {
    if (!confirm(`¿Eliminar lote "${lote.numeroLote}"?`)) return;
    this.eliminarLote.ejecutar(this.productoId()!, lote.id).subscribe({
      next: () => this.lotes.set(this.lotes().filter(l => l.id !== lote.id))
    });
  }
}
