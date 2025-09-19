namespace Canina.Application.Features.Profesionales;

public record ObtenerProfesionalesResponse(
    Guid id,
    string nombre_completo,
    string? especialidad
);