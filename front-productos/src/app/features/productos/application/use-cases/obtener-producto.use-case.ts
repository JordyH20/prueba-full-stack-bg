import { inject, Injectable } from '@angular/core';
import { PRODUCTO_REPOSITORY } from '../../domain/ports/producto.repository';

@Injectable()
export class ObtenerProductoUseCase {
  private readonly repo = inject(PRODUCTO_REPOSITORY);

  ejecutar(id: number) {
    return this.repo.obtener(id);
  }
}
