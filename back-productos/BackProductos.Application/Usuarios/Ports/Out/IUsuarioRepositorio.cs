using BackProductos.Domain.Usuarios;

namespace BackProductos.Application.Usuarios.Ports.Out;

public interface IUsuarioRepositorio
{
    Task<Users?> ObtenerPorCorreoAsync(string correo);
    Task<IReadOnlyList<Users>> ObtenerTodosAsync();
    Task<bool> ExisteCorreoAsync(string correo);
    Task<int> CrearAsync(Users usuario);
}
