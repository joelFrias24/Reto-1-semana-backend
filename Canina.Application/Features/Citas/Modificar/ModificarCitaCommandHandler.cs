using Canina.Domain.Contracts;
using ErrorOr;

namespace Canina.Application.Features.Citas.Modificar;

public class ModificarCitaCommandHandler
{
    private readonly ICitaRepository _citaRepository;
    private readonly IMascotaRepository _mascotaRepository;
    private readonly IDuenioRepository _duenioRepository;

    public ModificarCitaCommandHandler(ICitaRepository citaRepository, IMascotaRepository mascotaRepository, IDuenioRepository duenioRepository)
    {
        _citaRepository = citaRepository;
        _mascotaRepository = mascotaRepository;
        _duenioRepository = duenioRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(Guid id, ModificarCitaCommand command)
    {
        var cita = await _citaRepository.GetByIdAsync(id);
        if (cita is null)
            return Error.NotFound("Appointment.NotFound", "Cita no encontrada.");

        if (command.fecha_inicio < DateTime.UtcNow)
            return Error.Validation("Appointment.InvalidDate", "La cita no puede estar en el pasado.");

        if (command.fecha_final <= command.fecha_inicio)
            return Error.Validation("Appointment.InvalidRange", "La hora de fin debe ser mayor a la de inicio.");

        cita.Update(
            command.servicio_id,
            command.profesional_id,
            command.fecha_inicio,
            command.fecha_final,
            command.motivo,
            command.observaciones
        );

        await _citaRepository.UpdateAsync(cita);
        await _citaRepository.SaveChangesAsync();

        // Re-mapear para respuesta
        var mascota = await _mascotaRepository.GetByIdAsync(cita.MascotaId);
        var duenio = mascota is not null ? await _duenioRepository.GetByIdAsync(mascota.DuenioId) : null;

        return id;
    }
}
