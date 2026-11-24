using BackProductos.Application.Productos;

namespace BackProductos.Application.Productos.Ports.In;

public interface IProductoServicio
{
    Task<IReadOnlyList<ProductoResponseDto>> ObtenerTodosAsync();
    Task<ProductoResponseDto> ObtenerPorIdAsync(int id);
    Task<ProductoResponseDto> CrearAsync(ProductoRequest request);
    Task<ProductoResponseDto> ActualizarAsync(int id, ProductoRequest request);
    Task EliminarAsync(int id);
    Task<LoteProductoResponseDto> RegistrarLoteAsync(int productoId, LoteProductoRequest request);
    Task<IReadOnlyList<LoteProductoResponseDto>> ObtenerLotesAsync(int productoId);
}
