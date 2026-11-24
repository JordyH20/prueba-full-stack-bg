using BackProductos.Application.Usuarios;
using BackProductos.Application.Usuarios.Ports.In;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_productos.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioControlador(IUsuarioServicio usuarioServicio) : ControllerBase
{
    [HttpPost("registro")]
    [ProducesResponseType<UsuarioResponseDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<UsuarioResponseDto>> Registrar([FromBody] RegistrarUsuarioRequest request)
    {
        var usuario = await usuarioServicio.RegistrarAsync(request);
        return StatusCode(StatusCodes.Status201Created, usuario);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UsuarioResponseDto>>> ObtenerTodos() =>
        Ok(await usuarioServicio.ObtenerTodosAsync());
}
