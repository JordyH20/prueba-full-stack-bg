using BackProductos.Application.Productos;
using BackProductos.Application.Productos.Ports.In;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_productos.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductoControlador(IProductoServicio productoServicio) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductoResponseDto>>> ObtenerTodos() =>
        Ok(await productoServicio.ObtenerTodosAsync());

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductoResponseDto>> ObtenerPorId(int id) =>
        Ok(await productoServicio.ObtenerPorIdAsync(id));

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType<ProductoResponseDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<ProductoResponseDto>> Crear([FromBody] ProductoRequest request)
    {
        var producto = await productoServicio.CrearAsync(request);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = producto.Id }, producto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductoResponseDto>> Actualizar(int id, [FromBody] ProductoRequest request) =>
        Ok(await productoServicio.ActualizarAsync(id, request));

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await productoServicio.EliminarAsync(id);
        return NoContent();
    }

    [Authorize]
    [HttpGet("{productoId:int}/lotes")]
    public async Task<ActionResult<IReadOnlyList<LoteProductoResponseDto>>> ObtenerLotes(int productoId) =>
        Ok(await productoServicio.ObtenerLotesAsync(productoId));

    [Authorize(Roles = "Admin")]
    [HttpPost("{productoId:int}/lotes")]
    [ProducesResponseType<LoteProductoResponseDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<LoteProductoResponseDto>> RegistrarLote(int productoId, [FromBody] LoteProductoRequest request)
    {
        var lote = await productoServicio.RegistrarLoteAsync(productoId, request);
        return CreatedAtAction(nameof(ObtenerLotes), new { productoId }, lote);
    }
}
