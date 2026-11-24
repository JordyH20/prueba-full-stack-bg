using BackProductos.Application.Productos;
using BackProductos.Application.Productos.Ports.Out;
using BackProductos.Domain.Productos;
using BackProductos.Infrastructure.Shared;
using Dapper;
using Producto = BackProductos.Domain.Productos.Productos;

namespace BackProductos.Infrastructure.Productos;

public sealed class ProductoRepositorio(DapperData data) : IProductoRepositorio
{
    private const string SelectProducto =
        "SELECT p.Id, p.Codigo, p.Nombre, p.Descripcion, p.Estado, " +
        "CAST(COALESCE(SUM(CASE WHEN l.Estado = 'A' THEN l.Stock ELSE 0 END), 0) AS int) AS StockTotal, " +
        "p.FechaCreacion, p.FechaActualizacion " +
        "FROM dbo.Productos p LEFT JOIN dbo.LotesProductos l ON l.ProductoId = p.Id";

    public async Task<IReadOnlyList<ProductoResponseDto>> ObtenerTodosAsync(bool soloActivos = false)
    {
        var filtro = soloActivos
            ? " WHERE p.Estado = 'A' GROUP BY "
            : " WHERE p.Estado <> 'N' GROUP BY ";
        var sql = SelectProducto + filtro +
            " p.Id, p.Codigo, p.Nombre, p.Descripcion, p.Estado, p.FechaCreacion, p.FechaActualizacion ORDER BY p.Id";
        using var connection = data.CreateConnection();
        return (await connection.QueryAsync<ProductoResponseDto>(sql)).AsList();
    }

    public async Task<ProductoResponseDto?> ObtenerPorIdAsync(int id)
    {
        const string sql = SelectProducto +
            " WHERE p.Id = @Id AND p.Estado <> 'N' GROUP BY p.Id, p.Codigo, p.Nombre, p.Descripcion, p.Estado, p.FechaCreacion, p.FechaActualizacion";
        using var connection = data.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<ProductoResponseDto>(sql, new { Id = id });
    }

    public async Task<bool> ExisteCodigoAsync(string codigo, int? excluirId = null)
    {
        const string sql = "SELECT CASE WHEN EXISTS (" +
            "SELECT 1 FROM dbo.Productos WHERE Codigo = @Codigo AND (@ExcluirId IS NULL OR Id <> @ExcluirId)" +
            ") THEN 1 ELSE 0 END";
        using var connection = data.CreateConnection();
        return await connection.ExecuteScalarAsync<bool>(sql, new { Codigo = codigo, ExcluirId = excluirId });
    }

    public async Task<int> CrearAsync(Producto producto)
    {
        const string sql =
            "INSERT INTO dbo.Productos (Codigo, Nombre, Descripcion, Estado, FechaCreacion) " +
            "OUTPUT INSERTED.Id VALUES (@Codigo, @Nombre, @Descripcion, @Estado, @FechaCreacion)";
        using var connection = data.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, producto);
    }

    public async Task<bool> ActualizarAsync(Producto producto)
    {
        const string sql =
            "UPDATE dbo.Productos SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, " +
            "Estado = @Estado, FechaActualizacion = @FechaActualizacion WHERE Id = @Id";
        using var connection = data.CreateConnection();
        return await connection.ExecuteAsync(sql, producto) == 1;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        const string sql = "UPDATE dbo.Productos SET Estado = 'N', FechaActualizacion = GETUTCDATE() WHERE Id = @Id";
        using var connection = data.CreateConnection();
        return await connection.ExecuteAsync(sql, new { Id = id }) == 1;
    }

    public async Task<bool> TieneLotesAsync(int productoId)
    {
        const string sql =
            "SELECT CASE WHEN EXISTS (" +
            "SELECT 1 FROM dbo.LotesProductos WHERE ProductoId = @ProductoId AND Estado <> 'N'" +
            ") THEN 1 ELSE 0 END";
        using var connection = data.CreateConnection();
        return await connection.ExecuteScalarAsync<bool>(sql, new { ProductoId = productoId });
    }

    public async Task<int> CrearLoteAsync(LotesProductos lote)
    {
        const string sql =
            "INSERT INTO dbo.LotesProductos (ProductoId, NumeroLote, Precio, Stock, Estado, FechaIngreso) " +
            "OUTPUT INSERTED.Id VALUES (@ProductoId, @NumeroLote, @Precio, @Stock, @Estado, @FechaIngreso)";
        using var connection = data.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, lote);
    }

    public async Task<bool> ActualizarLoteAsync(LotesProductos lote)
    {
        const string sql =
            "UPDATE dbo.LotesProductos SET NumeroLote = @NumeroLote, Precio = @Precio, Stock = @Stock, " +
            "FechaActualizacion = @FechaActualizacion WHERE Id = @Id";
        using var connection = data.CreateConnection();
        return await connection.ExecuteAsync(sql, lote) == 1;
    }

    public async Task<bool> EliminarLoteAsync(int loteId)
    {
        const string sql = "UPDATE dbo.LotesProductos SET Estado = 'N', FechaActualizacion = GETUTCDATE() WHERE Id = @Id";
        using var connection = data.CreateConnection();
        return await connection.ExecuteAsync(sql, new { Id = loteId }) == 1;
    }

    public async Task<LotesProductos?> ObtenerLotePorIdAsync(int loteId)
    {
        const string sql =
            "SELECT Id, ProductoId, NumeroLote, Precio, Stock, Estado, FechaIngreso, FechaActualizacion " +
            "FROM dbo.LotesProductos WHERE Id = @Id";
        using var connection = data.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<LotesProductos>(sql, new { Id = loteId });
    }

    public async Task<IReadOnlyList<LoteProductoResponseDto>> ObtenerLotesAsync(int productoId)
    {
        const string sql =
            "SELECT Id, ProductoId, NumeroLote, Precio, Stock, Estado, FechaIngreso, FechaActualizacion " +
            "FROM dbo.LotesProductos WHERE ProductoId = @ProductoId AND Estado <> 'N' ORDER BY Id DESC";
        using var connection = data.CreateConnection();
        return (await connection.QueryAsync<LoteProductoResponseDto>(sql, new { ProductoId = productoId })).AsList();
    }
}
