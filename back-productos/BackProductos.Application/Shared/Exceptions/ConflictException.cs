namespace BackProductos.Application.Shared.Exceptions;

public sealed class ConflictException(string message) : Exception(message);
