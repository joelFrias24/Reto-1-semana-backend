using Canina.Domain.Contracts;
using Canina.Domain.Entities;
using ErrorOr;

namespace Canina.Application.Features.Citas.ChangeStatus;

public class ChangeStatusCommandHandler
{
    private readonly ICitaRepository _citaRepository;
    private readonly IMascotaRepository _mascotaRepository;
    private readonly IDuenioRepository _duenioRepository;

    public ChangeStatusCommandHandler(ICitaRepository citaRepository, IMascotaRepository mascotaRepository, IDuenioRepository duenioRepository)
    {
        _citaRepository = citaRepository;
        _mascotaRepository = mascotaRepository;
        _duenioRepository = duenioRepository;
    }

    public async Task<ErrorOr<ChangeStatusResponse>> Handle(ChangeStatusCommand command)
    {
        var cita = await _citaRepository.GetByIdAsync(command.Id);
        if (cita is null)
            return Error.NotFound("Appointment.NotFound", "Cita no encontrada.");

        if (cita.Estatus == EstatusCita.Cancelada)
            return Error.Conflict("Appointment.AlreadyCancelled", "La cita ya está cancelada.");

        cita.ChangeStatus(command.nuevo_estatus);

        await _citaRepository.UpdateAsync(cita);
        await _citaRepository.SaveChangesAsync();

        var mascota = await _mascotaRepository.GetByIdAsync(cita.MascotaId);
        var duenio = mascota is not null ? await _duenioRepository.GetByIdAsync(mascota.DuenioId) : null;

        return new ChangeStatusResponse(cita.Estatus.ToString());

    }
}
