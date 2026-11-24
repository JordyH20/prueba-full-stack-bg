using BackProductos.Application.Usuarios;
using BackProductos.Application.Usuarios.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace back_productos.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginControlador(IAuthServicio authServicio) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<LoginResponseDto>(StatusCodes.Status200OK)]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request) =>
        Ok(await authServicio.LoginAsync(request));
}
