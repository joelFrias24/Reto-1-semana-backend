using Canina.Domain.Contracts;
using Canina.Domain.Entities;
using ErrorOr;

namespace Canina.Application.Features.Citas.Crear;

public class CrearCitaCommandHandler
{
    private readonly ICitaRepository _citaRepository;
    private readonly IMascotaRepository _mascotaRepository;
    private readonly IDuenioRepository _duenioRepository;

    public CrearCitaCommandHandler(ICitaRepository citaRepository, IMascotaRepository mascotaRepository, IDuenioRepository duenioRepository)
    {
        _citaRepository = citaRepository;
        _mascotaRepository = mascotaRepository;
        _duenioRepository = duenioRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CrearCitaCommand request)
    {
        var mascota = await _mascotaRepository.GetByIdAsync(request.mascota_id);
        if (mascota is null)
            return Error.NotFound("Pet.NotFound", "Mascota no encontrada.");

        var duenio = await _duenioRepository.GetByIdAsync(mascota.DuenioId);
        if (duenio is null)
            return Error.NotFound("Owner.NotFound", "Dueño no encontrado.");

        if (request.fecha_hora_inicio < DateTime.UtcNow)
            return Error.Validation("Appointment.InvalidDate", "La cita no puede estar en el pasado.");

        if (request.fecha_hora_fin <= request.fecha_hora_inicio)
            return Error.Validation("Appointment.InvalidRange", "La hora de fin debe ser mayor a la de inicio.");

        var cita = new Cita(
            request.mascota_id,
            request.servicio_id,
            request.profesional_id,
            request.fecha_hora_inicio,
            request.fecha_hora_fin,
            request.motivo,
            request.observaciones
        );

        await _citaRepository.AddAsync(cita);
        await _citaRepository.SaveChangesAsync();

        return cita.Id;
    }
}
