import { Provider } from '@angular/core';
import { PRODUCTO_REPOSITORY } from './domain/ports/producto.repository';
import { HttpProductoRepository } from './infrastructure/adapters/http-producto.repository';
import { ListarProductosUseCase } from './application/use-cases/listar-productos.use-case';
import { ObtenerProductoUseCase } from './application/use-cases/obtener-producto.use-case';
import { GuardarProductoUseCase } from './application/use-cases/guardar-producto.use-case';
import { EliminarProductoUseCase } from './application/use-cases/eliminar-producto.use-case';
import { GuardarLoteUseCase } from './application/use-cases/guardar-lote.use-case';
import { EliminarLoteUseCase } from './application/use-cases/eliminar-lote.use-case';

export const productosProviders: Provider[] = [
  { provide: PRODUCTO_REPOSITORY, useClass: HttpProductoRepository },
  ListarProductosUseCase,
  ObtenerProductoUseCase,
  GuardarProductoUseCase,
  EliminarProductoUseCase,
  GuardarLoteUseCase,
  EliminarLoteUseCase
];
