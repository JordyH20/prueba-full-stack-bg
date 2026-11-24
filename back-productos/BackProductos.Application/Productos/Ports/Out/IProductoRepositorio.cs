using BackProductos.Application.Productos;
using BackProductos.Domain.Productos;
using Producto = BackProductos.Domain.Productos.Productos;

namespace BackProductos.Application.Productos.Ports.Out;

public interface IProductoRepositorio
{
    Task<IReadOnlyList<ProductoResponseDto>> ObtenerTodosAsync(bool soloActivos = false);
    Task<ProductoResponseDto?> ObtenerPorIdAsync(int id);
    Task<bool> ExisteCodigoAsync(string codigo, int? excluirId = null);
    Task<int> CrearAsync(Producto producto);
    Task<bool> ActualizarAsync(Producto producto);
    Task<bool> EliminarAsync(int id);
    Task<bool> TieneLotesAsync(int productoId);
    Task<int> CrearLoteAsync(LotesProductos lote);
    Task<bool> ActualizarLoteAsync(LotesProductos lote);
    Task<bool> EliminarLoteAsync(int loteId);
    Task<LotesProductos?> ObtenerLotePorIdAsync(int loteId);
    Task<IReadOnlyList<LoteProductoResponseDto>> ObtenerLotesAsync(int productoId);
}
