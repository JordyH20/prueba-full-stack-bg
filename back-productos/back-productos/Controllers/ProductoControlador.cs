using BackProductos.Application.Productos;
using BackProductos.Application.Productos.Ports.In;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_productos.Controllers;

[Route("api/productos")]
[ApiController]
public class ProductoControlador(IProductoServicio productoServicio) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductoResponseDto>>> ObtenerTodos()
    {
        var esAdmin = User.IsInRole("Admin");
        return Ok(await productoServicio.ObtenerTodosAsync(!esAdmin));
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductoDetalleDto>> ObtenerPorId(int id) =>
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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

    [Authorize(Roles = "Admin")]
    [HttpPut("{productoId:int}/lotes/{loteId:int}")]
    public async Task<ActionResult<LoteProductoResponseDto>> ActualizarLote(int productoId, int loteId, [FromBody] LoteProductoRequest request) =>
        Ok(await productoServicio.ActualizarLoteAsync(productoId, loteId, request));

    [Authorize(Roles = "Admin")]
    [HttpDelete("{productoId:int}/lotes/{loteId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> EliminarLote(int productoId, int loteId)
    {
        await productoServicio.EliminarLoteAsync(productoId, loteId);
        return NoContent();
    }
}
