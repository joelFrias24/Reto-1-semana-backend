namespace Canina.Application.Features.Mascotas.Crear;

public record CrearMascotaCommand(
    string nombre,
    string especie,
    string raza,
    DateOnly? fecha_nacimiento,
    string sexo,
    string? color,
    decimal? peso,
    string? notas,
    Guid duenio_id
);
