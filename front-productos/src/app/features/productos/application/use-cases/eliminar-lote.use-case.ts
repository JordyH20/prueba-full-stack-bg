import { inject, Injectable } from '@angular/core';
import { PRODUCTO_REPOSITORY } from '../../domain/ports/producto.repository';

@Injectable()
export class EliminarLoteUseCase {
  private readonly repo = inject(PRODUCTO_REPOSITORY);

  ejecutar(productoId: number, loteId: number) {
    return this.repo.eliminarLote(productoId, loteId);
  }
}
