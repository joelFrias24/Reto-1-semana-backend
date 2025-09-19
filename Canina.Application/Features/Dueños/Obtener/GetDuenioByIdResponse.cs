using Canina.Application.Features.Dueños.Queries;

namespace Canina.Application.Features.Dueños.Obtener;

public record GetDuenioByIdResponse(
    Guid id,
    string nombre_completo,
    string num_identificacion,
    string? direccion,
    string? telefono,
    string? correo
);
