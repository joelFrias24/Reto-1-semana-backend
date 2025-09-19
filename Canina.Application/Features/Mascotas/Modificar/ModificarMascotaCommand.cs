namespace Canina.Application.Features.Mascotas.Modificar;

public record ModificarMascotaCommand(
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