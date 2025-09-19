using Canina.Domain.Contracts;
using ErrorOr;
using System.Net;

namespace Canina.Application.Features.Mascotas.Modificar;

public class ModificarMascotaCommandHandler
{
    private readonly IMascotaRepository _mascotaRepository;

    public ModificarMascotaCommandHandler(IMascotaRepository mascotaRepository)
    {
        _mascotaRepository = mascotaRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(Guid id, ModificarMascotaCommand request)
    {
        var mascota = await _mascotaRepository.GetByIdAsync(id);
        if (mascota is null)
            return Error.NotFound("Pet.NotFound", "Mascota no encontrada.");

        mascota.Update(
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

        await _mascotaRepository.UpdateAsync(mascota);
        await _mascotaRepository.SaveChangesAsync();

        return id;
    }
}