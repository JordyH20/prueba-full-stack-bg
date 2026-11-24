using BackProductos.Application.Usuarios;

namespace BackProductos.Application.Usuarios.Ports.In;

public interface IUsuarioServicio
{
    Task<UsuarioResponseDto> RegistrarAsync(RegistrarUsuarioRequest request);
    Task<IReadOnlyList<UsuarioResponseDto>> ObtenerTodosAsync();
}
