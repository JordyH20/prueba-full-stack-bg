using BackProductos.Application.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace back_productos.Middleware;

public sealed class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var (status, title) = exception switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, "Recurso no encontrado"),
                ConflictException => (StatusCodes.Status409Conflict, "Conflicto"),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "No autorizado"),
                _ => (StatusCodes.Status500InternalServerError, "Error interno del servidor")
            };

            if (status >= 500)
                logger.LogError(exception, "Error no controlado procesando {Method} {Path}", context.Request.Method, context.Request.Path);
            else
                logger.LogInformation(exception, "Solicitud rechazada con estado {StatusCode}", status);

            if (context.Response.HasStarted)
                throw;

            context.Response.Clear();
            context.Response.StatusCode = status;
            context.Response.ContentType = "application/problem+json";
            var problem = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = status >= 500 ? "Ocurrió un error inesperado." : exception.Message,
                Instance = context.Request.Path
            };
            problem.Extensions["traceId"] = context.TraceIdentifier;
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
