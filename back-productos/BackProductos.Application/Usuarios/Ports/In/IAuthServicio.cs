using BackProductos.Application.Usuarios;

namespace BackProductos.Application.Usuarios.Ports.In;

public interface IAuthServicio
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
}
