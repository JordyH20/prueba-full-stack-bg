using BackProductos.Application.Productos.Ports.In;
using BackProductos.Application.Productos.Ports.Out;
using BackProductos.Application.Shared.Exceptions;
using BackProductos.Domain.Productos;
using Producto = BackProductos.Domain.Productos.Productos;

namespace BackProductos.Application.Productos.UseCases
{
    public class ProductoUseCase : IProductoServicio
    {
        private readonly IProductoRepositorio _repositorio;

        public ProductoUseCase(IProductoRepositorio repositorio) => _repositorio = repositorio;

        public Task<IReadOnlyList<ProductoResponseDto>> ObtenerTodosAsync(bool soloActivos = false) =>
            _repositorio.ObtenerTodosAsync(soloActivos);

        public async Task<ProductoDetalleDto> ObtenerPorIdAsync(int id)
        {
            var producto = await _repositorio.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException($"No existe el producto {id}.");
            var lotes = await _repositorio.ObtenerLotesAsync(id);
            return new ProductoDetalleDto
            {
                Id = producto.Id,
                Codigo = producto.Codigo,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Estado = producto.Estado,
                StockTotal = producto.StockTotal,
                FechaCreacion = producto.FechaCreacion,
                FechaActualizacion = producto.FechaActualizacion,
                Lotes = lotes
            };
        }

        public async Task<ProductoResponseDto> CrearAsync(ProductoRequest request)
        {
            var codigo = request.Codigo.Trim();
            if (await _repositorio.ExisteCodigoAsync(codigo))
                throw new ConflictException("Ya existe un producto con ese código.");

            var producto = new Producto
            {
                Codigo = codigo,
                Nombre = request.Nombre.Trim(),
                Descripcion = request.Descripcion.Trim(),
                Estado = "A",
                FechaCreacion = DateTime.UtcNow
            };
            var id = await _repositorio.CrearAsync(producto);
            return await _repositorio.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException($"No existe el producto {id}.");
        }

        public async Task<ProductoResponseDto> ActualizarAsync(int id, ProductoRequest request)
        {
            var actual = await _repositorio.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException($"No existe el producto {id}.");
            var codigo = request.Codigo.Trim();
            if (await _repositorio.ExisteCodigoAsync(codigo, id))
                throw new ConflictException("Ya existe un producto con ese código.");

            var producto = new Producto
            {
                Id = actual.Id,
                Codigo = codigo,
                Nombre = request.Nombre.Trim(),
                Descripcion = request.Descripcion.Trim(),
                Estado = request.Estado,
                FechaCreacion = actual.FechaCreacion,
                FechaActualizacion = DateTime.UtcNow
            };
            await _repositorio.ActualizarAsync(producto);
            return await _repositorio.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException($"No existe el producto {id}.");
        }

        public async Task EliminarAsync(int id)
        {
            _ = await _repositorio.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException($"No existe el producto {id}.");
            if (await _repositorio.TieneLotesAsync(id))
                throw new ConflictException("No se puede eliminar el producto porque tiene lotes asociados.");
            if (!await _repositorio.EliminarAsync(id))
                throw new NotFoundException($"No existe el producto {id}.");
        }

        public async Task<LoteProductoResponseDto> RegistrarLoteAsync(int productoId, LoteProductoRequest request)
        {
            _ = await _repositorio.ObtenerPorIdAsync(productoId)
                ?? throw new NotFoundException($"No existe el producto {productoId}.");
            var lote = new LotesProductos
            {
                ProductoId = productoId,
                NumeroLote = request.NumeroLote.Trim(),
                Precio = request.Precio,
                Stock = request.Stock,
                Estado = "A",
                FechaIngreso = DateTime.UtcNow
            };
            lote.Id = await _repositorio.CrearLoteAsync(lote);
            return (await _repositorio.ObtenerLotesAsync(productoId)).Single(l => l.Id == lote.Id);
        }

        public async Task<LoteProductoResponseDto> ActualizarLoteAsync(int productoId, int loteId, LoteProductoRequest request)
        {
            _ = await _repositorio.ObtenerPorIdAsync(productoId)
                ?? throw new NotFoundException($"No existe el producto {productoId}.");
            var loteActual = await _repositorio.ObtenerLotePorIdAsync(loteId)
                ?? throw new NotFoundException($"No existe el lote {loteId}.");
            if (loteActual.ProductoId != productoId)
                throw new NotFoundException($"No existe el lote {loteId}.");

            var lote = new LotesProductos
            {
                Id = loteId,
                ProductoId = productoId,
                NumeroLote = request.NumeroLote.Trim(),
                Precio = request.Precio,
                Stock = request.Stock,
                Estado = loteActual.Estado,
                FechaIngreso = loteActual.FechaIngreso,
                FechaActualizacion = DateTime.UtcNow
            };
            await _repositorio.ActualizarLoteAsync(lote);
            return (await _repositorio.ObtenerLotesAsync(productoId)).Single(l => l.Id == loteId);
        }

        public async Task EliminarLoteAsync(int productoId, int loteId)
        {
            _ = await _repositorio.ObtenerPorIdAsync(productoId)
                ?? throw new NotFoundException($"No existe el producto {productoId}.");
            var lote = await _repositorio.ObtenerLotePorIdAsync(loteId)
                ?? throw new NotFoundException($"No existe el lote {loteId}.");
            if (lote.ProductoId != productoId)
                throw new NotFoundException($"No existe el lote {loteId}.");
            if (!await _repositorio.EliminarLoteAsync(loteId))
                throw new NotFoundException($"No existe el lote {loteId}.");
        }

        public async Task<IReadOnlyList<LoteProductoResponseDto>> ObtenerLotesAsync(int productoId)
        {
            _ = await _repositorio.ObtenerPorIdAsync(productoId)
                ?? throw new NotFoundException($"No existe el producto {productoId}.");
            return await _repositorio.ObtenerLotesAsync(productoId);
        }
    }
}
