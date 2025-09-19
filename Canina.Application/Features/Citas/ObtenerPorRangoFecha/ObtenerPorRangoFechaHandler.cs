using Canina.Domain.Contracts;
using ErrorOr;

namespace Canina.Application.Features.Citas.ObtenerPorRangoFecha;

public class ObtenerPorRangoFechaHandler
{
    private readonly ICitaRepository _citaRepository;

    public ObtenerPorRangoFechaHandler(ICitaRepository citaRepository)
    {
        _citaRepository = citaRepository;
    }

    public async Task<ErrorOr<List<ObtenerPorRangoFechaResponse>>> Handle(DateTime request)
    {
        // El rango es desde el lunes de esa semana hasta el domingo
        var weekStart = request;
        var weekEnd = weekStart.AddDays(7).AddTicks(-1);

        var citas = await _citaRepository.GetByDateRangeAsync(weekStart, weekEnd);

        var respuestas = citas.Select(c => new ObtenerPorRangoFechaResponse(
            c.Id.ToString(),
            c.MascotaId.ToString(),
            c.Mascota.Nombre,
            c.ServicioId.ToString(),
            c.Servicio?.Nombre ?? string.Empty,
            c.ProfesionalId.ToString(),
            c.Profesional?.NombreCompleto,
            c.FechaHoraInicio,
            c.FechaHoraFin,
            c.Motivo,
            c.Estatus.ToString(),
            c.Observaciones
        )).ToList();

        return respuestas;
    }
}