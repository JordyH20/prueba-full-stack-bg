using BackProductos.Application.Productos;

namespace BackProductos.Application.Productos.Ports.In;

public interface IProductoServicio
{
    Task<IReadOnlyList<ProductoResponseDto>> ObtenerTodosAsync(bool soloActivos = false);
    Task<ProductoDetalleDto> ObtenerPorIdAsync(int id);
    Task<ProductoResponseDto> CrearAsync(ProductoRequest request);
    Task<ProductoResponseDto> ActualizarAsync(int id, ProductoRequest request);
    Task EliminarAsync(int id);
    Task<LoteProductoResponseDto> RegistrarLoteAsync(int productoId, LoteProductoRequest request);
    Task<LoteProductoResponseDto> ActualizarLoteAsync(int productoId, int loteId, LoteProductoRequest request);
    Task EliminarLoteAsync(int productoId, int loteId);
    Task<IReadOnlyList<LoteProductoResponseDto>> ObtenerLotesAsync(int productoId);
}
