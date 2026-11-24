using BackProductos.Application.Usuarios.Ports.Out;
using BackProductos.Domain.Usuarios;
using BackProductos.Infrastructure.Shared;
using Dapper;

namespace BackProductos.Infrastructure.Usuarios;

public sealed class UsuarioRepositorio(DapperData data) : IUsuarioRepositorio
{
    private const string ColumnasUsuario = "u.Id, u.Nombre, u.Correo, u.[ContraseñaHash] AS ContrasenaHash, u.RoleId, r.Nombre AS RolNombre, u.FechaCreacion";

    public async Task<Users?> ObtenerPorCorreoAsync(string correo)
    {
        const string sql = $"SELECT {ColumnasUsuario} FROM dbo.Users u INNER JOIN dbo.Roles r ON r.Id = u.RoleId WHERE u.Correo = @Correo";
        using var connection = data.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Users>(sql, new { Correo = correo });
    }

    public async Task<IReadOnlyList<Users>> ObtenerTodosAsync()
    {
        const string sql = "SELECT u.Id, u.Nombre, u.Correo, u.RoleId, r.Nombre AS RolNombre, u.FechaCreacion FROM dbo.Users u INNER JOIN dbo.Roles r ON r.Id = u.RoleId ORDER BY u.Id";
        using var connection = data.CreateConnection();
        var usuarios = await connection.QueryAsync<Users>(sql);
        return usuarios.AsList();
    }

    public async Task<bool> ExisteCorreoAsync(string correo)
    {
        const string sql = "SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Users WHERE Correo = @Correo) THEN 1 ELSE 0 END";
        using var connection = data.CreateConnection();
        return await connection.ExecuteScalarAsync<bool>(sql, new { Correo = correo });
    }

    public async Task<int> CrearAsync(Users usuario)
    {
        const string sql = "INSERT INTO dbo.Users (Nombre, Correo, [ContraseñaHash], RoleId, FechaCreacion) OUTPUT INSERTED.Id VALUES (@Nombre, @Correo, @ContrasenaHash, @RoleId, @FechaCreacion)";
        using var connection = data.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, usuario);
    }
}
