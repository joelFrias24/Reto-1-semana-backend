namespace Canina.Application.Features.Citas.Crear;

public record CrearCitaCommand(
    Guid mascota_id,
    Guid servicio_id,
    Guid? profesional_id,
    DateTime fecha_hora_inicio,
    DateTime fecha_hora_fin,
    string? motivo,
    string? observaciones
);