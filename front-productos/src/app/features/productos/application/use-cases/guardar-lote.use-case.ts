import { inject, Injectable } from '@angular/core';
import { LoteForm, PRODUCTO_REPOSITORY } from '../../domain/ports/producto.repository';

@Injectable()
export class GuardarLoteUseCase {
  private readonly repo = inject(PRODUCTO_REPOSITORY);

  crear(productoId: number, form: LoteForm) {
    return this.repo.crearLote(productoId, form);
  }

  actualizar(productoId: number, loteId: number, form: LoteForm) {
    return this.repo.actualizarLote(productoId, loteId, form);
  }
}
