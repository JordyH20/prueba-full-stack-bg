import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { API_URL } from '../../../../core/config/api.config';
import { ProductoRepository, ProductoForm, LoteForm } from '../../domain/ports/producto.repository';
import { Producto, ProductoDetalle } from '../../domain/models/producto';
import { Lote } from '../../domain/models/lote';
import { LoteDto, ProductoDetalleDto, ProductoDto } from '../dto/producto.dto';
import { mapLote, mapProducto, mapProductoDetalle } from '../mappers/producto.mapper';

@Injectable()
export class HttpProductoRepository implements ProductoRepository {
  private readonly http = inject(HttpClient);
  private readonly base = inject(API_URL);

  private get url() { return `${this.base}/productos`; }

  listar(): Observable<Producto[]> {
    return this.http.get<ProductoDto[]>(this.url).pipe(map(list => list.map(mapProducto)));
  }

  obtener(id: number): Observable<ProductoDetalle> {
    return this.http.get<ProductoDetalleDto>(`${this.url}/${id}`).pipe(map(mapProductoDetalle));
  }

  crear(form: ProductoForm): Observable<Producto> {
    return this.http.post<ProductoDto>(this.url, form).pipe(map(mapProducto));
  }

  actualizar(id: number, form: ProductoForm): Observable<Producto> {
    return this.http.put<ProductoDto>(`${this.url}/${id}`, form).pipe(map(mapProducto));
  }

  eliminar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }

  crearLote(productoId: number, form: LoteForm): Observable<Lote> {
    return this.http.post<LoteDto>(`${this.url}/${productoId}/lotes`, form).pipe(map(mapLote));
  }

  actualizarLote(productoId: number, loteId: number, form: LoteForm): Observable<Lote> {
    return this.http.put<LoteDto>(`${this.url}/${productoId}/lotes/${loteId}`, form).pipe(map(mapLote));
  }

  eliminarLote(productoId: number, loteId: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${productoId}/lotes/${loteId}`);
  }
}
