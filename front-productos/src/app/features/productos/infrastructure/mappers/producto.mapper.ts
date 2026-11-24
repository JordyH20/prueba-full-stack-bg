import { Lote } from '../../domain/models/lote';
import { Producto, ProductoDetalle } from '../../domain/models/producto';
import { LoteDto, ProductoDetalleDto, ProductoDto } from '../dto/producto.dto';

export function mapProducto(dto: ProductoDto): Producto {
  return {
    id: dto.id,
    codigo: dto.codigo,
    nombre: dto.nombre,
    descripcion: dto.descripcion,
    estado: dto.estado,
    stockTotal: dto.stockTotal,
    fechaCreacion: dto.fechaCreacion,
    fechaActualizacion: dto.fechaActualizacion
  };
}

export function mapProductoDetalle(dto: ProductoDetalleDto): ProductoDetalle {
  return {
    ...mapProducto(dto),
    lotes: dto.lotes.map(mapLote)
  };
}

export function mapLote(dto: LoteDto): Lote {
  return {
    id: dto.id,
    productoId: dto.productoId,
    numeroLote: dto.numeroLote,
    precio: dto.precio,
    stock: dto.stock,
    estado: dto.estado,
    fechaIngreso: dto.fechaIngreso,
    fechaActualizacion: dto.fechaActualizacion
  };
}
