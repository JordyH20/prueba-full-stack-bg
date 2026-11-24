import { inject, Injectable } from '@angular/core';
import { PRODUCTO_REPOSITORY } from '../../domain/ports/producto.repository';

@Injectable()
export class ListarProductosUseCase {
  private readonly repo = inject(PRODUCTO_REPOSITORY);

  ejecutar() {
    return this.repo.listar();
  }
}
