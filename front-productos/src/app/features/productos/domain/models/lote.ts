export interface Lote {
  id: number;
  productoId: number;
  numeroLote: string;
  precio: number;
  stock: number;
  estado: string;
  fechaIngreso: string;
  fechaActualizacion: string | null;
}
