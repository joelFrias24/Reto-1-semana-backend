using Canina.Domain.Contracts;
using Canina.Domain.Entities;
using ErrorOr;

namespace Canina.Application.Features.Mascotas.Crear;

public class CrearMascotaCommandHandler
{
    private readonly IMascotaRepository _mascotaRepository;
    private readonly IDuenioRepository _duenioRepository;

    public CrearMascotaCommandHandler(IMascotaRepository mascotaRepository, IDuenioRepository duenioRepository)
    {
        _mascotaRepository = mascotaRepository;
        _duenioRepository = duenioRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CrearMascotaCommand request)
    {
        var duenio = await _duenioRepository.GetByIdAsync(request.duenio_id);
        if (duenio is null)
            return Error.NotFound("Owner.NotFound", "Dueño no encontrado.");

        var mascota = new Mascota(
            request.nombre,
            request.especie,
            request.raza,
            request.fecha_nacimiento,
            request.sexo,
            request.color,
            request.peso,
            request.notas,
            request.duenio_id
        );

        await _mascotaRepository.AddAsync(mascota);
        await _mascotaRepository.SaveChangesAsync();

        return mascota.Id;
    }
}
