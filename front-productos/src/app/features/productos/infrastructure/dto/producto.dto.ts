export interface LoteDto {
  id: number;
  productoId: number;
  numeroLote: string;
  precio: number;
  stock: number;
  estado: string;
  fechaIngreso: string;
  fechaActualizacion: string | null;
}

export interface ProductoDto {
  id: number;
  codigo: string;
  nombre: string;
  descripcion: string;
  estado: string;
  stockTotal: number;
  fechaCreacion: string;
  fechaActualizacion: string | null;
}

export interface ProductoDetalleDto extends ProductoDto {
  lotes: LoteDto[];
}
