using BackProductos.Application.Productos.Ports.In;
using BackProductos.Application.Productos.Ports.Out;
using BackProductos.Application.Shared.Exceptions;
using BackProductos.Domain.Productos;

namespace BackProductos.Application.Productos.UseCases
{
    public class ProductoUseCase : IProductoServicio
    {
        private readonly IProductoRepositorio _repositorio;

        public ProductoUseCase(IProductoRepositorio repositorio) => _repositorio = repositorio;

        public Task<IReadOnlyList<ProductoResponseDto>> ObtenerTodosAsync() => _repositorio.ObtenerTodosAsync();

        public async Task<ProductoResponseDto> ObtenerPorIdAsync(int id) =>
            await _repositorio.ObtenerPorIdAsync(id) ?? throw new NotFoundException($"No existe el producto {id}.");

        public async Task<ProductoResponseDto> CrearAsync(ProductoRequest request)
        {
            var codigo = request.Codigo.Trim();
            if (await _repositorio.ExisteCodigoAsync(codigo))
                throw new ConflictException("Ya existe un producto con ese código.");

            var producto = new BackProductos.Domain.Productos.Productos
            {
                Codigo = codigo,
                Nombre = request.Nombre.Trim(),
                Descripcion = request.Descripcion.Trim(),
                Estado = true,
                FechaCreacion = DateTime.UtcNow
            };
            var id = await _repositorio.CrearAsync(producto);
            return await ObtenerPorIdAsync(id);
        }

        public async Task<ProductoResponseDto> ActualizarAsync(int id, ProductoRequest request)
        {
            var actual = await _repositorio.ObtenerPorIdAsync(id)
                ?? throw new NotFoundException($"No existe el producto {id}.");
            var codigo = request.Codigo.Trim();
            if (await _repositorio.ExisteCodigoAsync(codigo, id))
                throw new ConflictException("Ya existe un producto con ese código.");

            var producto = new BackProductos.Domain.Productos.Productos
            {
                Id = actual.Id,
                Codigo = codigo,
                Nombre = request.Nombre.Trim(),
                Descripcion = request.Descripcion.Trim(),
                Estado = actual.Estado,
                FechaCreacion = actual.FechaCreacion,
                FechaActualizacion = DateTime.UtcNow
            };
            await _repositorio.ActualizarAsync(producto);
            return await ObtenerPorIdAsync(id);
        }

        public async Task EliminarAsync(int id)
        {
            _ = await ObtenerPorIdAsync(id);
            if (await _repositorio.TieneLotesAsync(id))
                throw new ConflictException("No se puede eliminar el producto porque tiene lotes asociados.");
            if (!await _repositorio.EliminarAsync(id))
                throw new NotFoundException($"No existe el producto {id}.");
        }

        public async Task<LoteProductoResponseDto> RegistrarLoteAsync(int productoId, LoteProductoRequest request)
        {
            _ = await ObtenerPorIdAsync(productoId);
            var lote = new LotesProductos
            {
                ProductoId = productoId,
                NumeroLote = request.NumeroLote.Trim(),
                Precio = request.Precio,
                Stock = request.Stock,
                Estado = true,
                FechaIngreso = DateTime.UtcNow
            };
            lote.Id = await _repositorio.CrearLoteAsync(lote);
            return (await _repositorio.ObtenerLotesAsync(productoId)).Single(l => l.Id == lote.Id);
        }

        public async Task<IReadOnlyList<LoteProductoResponseDto>> ObtenerLotesAsync(int productoId)
        {
            _ = await ObtenerPorIdAsync(productoId);
            return await _repositorio.ObtenerLotesAsync(productoId);
        }
    }
}
