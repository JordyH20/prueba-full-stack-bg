export interface Lote {
  id: number;
  productoId: number;
  numeroLote: string;
  precio: number;
  stock: number;
  estado: boolean;
  fechaIngreso: string;
  fechaActualizacion: string | null;
}
