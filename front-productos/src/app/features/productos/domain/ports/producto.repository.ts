import { InjectionToken } from '@angular/core';
import { Observable } from 'rxjs';
import { Producto, ProductoDetalle } from '../models/producto';
import { Lote } from '../models/lote';

export interface ProductoForm {
  codigo: string;
  nombre: string;
  descripcion: string;
}

export interface LoteForm {
  numeroLote: string;
  precio: number;
  stock: number;
}

export interface ProductoRepository {
  listar(): Observable<Producto[]>;
  obtener(id: number): Observable<ProductoDetalle>;
  crear(form: ProductoForm): Observable<Producto>;
  actualizar(id: number, form: ProductoForm): Observable<Producto>;
  eliminar(id: number): Observable<void>;
  crearLote(productoId: number, form: LoteForm): Observable<Lote>;
  actualizarLote(productoId: number, loteId: number, form: LoteForm): Observable<Lote>;
  eliminarLote(productoId: number, loteId: number): Observable<void>;
}

export const PRODUCTO_REPOSITORY = new InjectionToken<ProductoRepository>('PRODUCTO_REPOSITORY');
