namespace Canina.Application.Features.Citas.Modificar;

public record ModificarCitaCommand(
    Guid mascota_id,
    Guid servicio_id,
    Guid? profesional_id,
    DateTime fecha_inicio,
    DateTime fecha_final,
    string? motivo,
    string? observaciones
);