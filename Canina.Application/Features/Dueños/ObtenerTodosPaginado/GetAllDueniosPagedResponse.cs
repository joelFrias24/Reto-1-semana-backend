namespace Canina.Application.Features.Dueños.ObtenerTodosPaginado;

public record MascotaResponse(
    Guid id,
    string nombre
);

public record GetAllDueniosPagedResponse(
    Guid id,
    string nombre_completo,
    string num_identificacion,
    string? direccion,
    string? telefono,
    string? correo,
    ICollection<MascotaResponse> mascotas
);