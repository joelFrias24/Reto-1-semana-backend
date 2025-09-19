namespace Canina.Application.Features.Mascotas.ObtenerTodos;

public record ObtenerMascotasResponse(
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