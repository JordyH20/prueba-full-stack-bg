import { Lote } from './lote';

export interface Producto {
  id: number;
  codigo: string;
  nombre: string;
  descripcion: string;
  estado: string;
  stockTotal: number;
  fechaCreacion: string;
  fechaActualizacion: string | null;
}

export interface ProductoDetalle extends Producto {
  lotes: Lote[];
}
