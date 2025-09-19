namespace Canina.Application.Features.Citas.ObtenerPorRangoFecha;

public record ObtenerPorRangoFechaResponse(
    string id,
    string mascota_id,
    string nombre,
    string servicio_id,
    string servicio,
    string? profesional_id,
    string? profesional,
    DateTime fecha_hora_inicio,
    DateTime fecha_hora_fin,
    string? motivo,
    string estatus,
    string? observaciones
);