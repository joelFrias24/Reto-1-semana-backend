namespace Canina.Application.Features.Servicios;

public record ObtenerServiciosResponse(
    Guid id,
    string nombre,
    string? descripcion
);
