import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Producto } from '../../../domain/models/producto';
import { ListarProductosUseCase } from '../../../application/use-cases/listar-productos.use-case';
import { EliminarProductoUseCase } from '../../../application/use-cases/eliminar-producto.use-case';
import { TablaLotesComponent } from '../../components/tabla-lotes/tabla-lotes';
import { ObtenerProductoUseCase } from '../../../application/use-cases/obtener-producto.use-case';
import { Lote } from '../../../domain/models/lote';

@Component({
  selector: 'app-productos-lista',
  imports: [RouterLink, FormsModule, TablaLotesComponent],
  templateUrl: './productos-lista.html',
  styleUrl: './productos-lista.css'
})
export class ProductosListaComponent implements OnInit {
  private readonly listar = inject(ListarProductosUseCase);
  private readonly eliminarUC = inject(EliminarProductoUseCase);
  private readonly obtener = inject(ObtenerProductoUseCase);

  readonly esAdmin = sessionStorage.getItem('rol') === 'Admin';
  readonly cargando = signal(false);
  readonly productos = signal<Producto[]>([]);
  readonly textoBusqueda = signal('');
  readonly lotesDesplegados = signal<{ [id: number]: Lote[] }>({});
  readonly cargandoLotes = signal<number | null>(null);

  ngOnInit(): void {
    this.cargar();
  }

  cargar(): void {
    this.cargando.set(true);
    this.listar.ejecutar().subscribe({
      next: lista => { this.productos.set(lista); this.cargando.set(false); },
      error: () => this.cargando.set(false)
    });
  }

  get productosFiltrados(): Producto[] {
    const texto = this.textoBusqueda().toLowerCase().trim();
    if (!texto) return this.productos();
    return this.productos().filter(p =>
      p.codigo.toLowerCase().includes(texto) || p.nombre.toLowerCase().includes(texto)
    );
  }

  toggleLotes(id: number): void {
    const actual = this.lotesDesplegados();
    if (actual[id]) {
      const { [id]: _, ...resto } = actual;
      this.lotesDesplegados.set(resto);
      return;
    }
    this.cargandoLotes.set(id);
    this.obtener.ejecutar(id).subscribe({
      next: detalle => {
        this.lotesDesplegados.set({ ...this.lotesDesplegados(), [id]: detalle.lotes });
        this.cargandoLotes.set(null);
      },
      error: () => this.cargandoLotes.set(null)
    });
  }

  confirmarEliminar(p: Producto): void {
    if (!confirm(`¿Eliminar "${p.nombre}"?`)) return;
    this.eliminarUC.ejecutar(p.id).subscribe({ next: () => this.cargar() });
  }
}
