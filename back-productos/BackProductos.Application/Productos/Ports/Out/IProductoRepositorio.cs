using BackProductos.Application.Productos;
using BackProductos.Domain.Productos;

namespace BackProductos.Application.Productos.Ports.Out;

public interface IProductoRepositorio
{
    Task<IReadOnlyList<ProductoResponseDto>> ObtenerTodosAsync();
    Task<ProductoResponseDto?> ObtenerPorIdAsync(int id);
    Task<bool> ExisteCodigoAsync(string codigo, int? excluirId = null);
    Task<int> CrearAsync(BackProductos.Domain.Productos.Productos producto);
    Task<bool> ActualizarAsync(BackProductos.Domain.Productos.Productos producto);
    Task<bool> EliminarAsync(int id);
    Task<bool> TieneLotesAsync(int productoId);
    Task<int> CrearLoteAsync(LotesProductos lote);
    Task<IReadOnlyList<LoteProductoResponseDto>> ObtenerLotesAsync(int productoId);
}
