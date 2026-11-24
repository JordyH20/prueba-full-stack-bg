import { inject, Injectable } from '@angular/core';
import { ProductoForm, PRODUCTO_REPOSITORY } from '../../domain/ports/producto.repository';

@Injectable()
export class GuardarProductoUseCase {
  private readonly repo = inject(PRODUCTO_REPOSITORY);

  crear(form: ProductoForm) {
    return this.repo.crear(form);
  }

  actualizar(id: number, form: ProductoForm) {
    return this.repo.actualizar(id, form);
  }
}
