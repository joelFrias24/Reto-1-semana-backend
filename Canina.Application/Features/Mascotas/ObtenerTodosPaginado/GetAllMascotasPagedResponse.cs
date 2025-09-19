namespace Canina.Application.Features.Mascotas.ObtenerTodosPaginado;

public record GetAllMascotasPagedResponse(
    Guid id,
    string nombre,
    string especie,
    string raza,
    DateOnly? fecha_nacimiento,
    string sexo,
    string? color,
    decimal? peso,
    string? notas,
    string duenio_id,
    string duenio
);
