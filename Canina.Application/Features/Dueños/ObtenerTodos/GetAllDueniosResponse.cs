using Canina.Application.Features.Dueños.Queries;

namespace Canina.Application.Features.Dueños.ObtenerTodos;

public record GetAllDueniosResponse(
    Guid id,
    string nombre_completo,
    string num_identificacion,
    string? direccion,
    string? telefono,
    string? correo
);
