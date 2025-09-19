using Canina.Domain.Contracts;
using ErrorOr;

namespace Canina.Application.Features.Dueños.Modificar;

public class ModificarDuenioCommandHandler
{
    private readonly IDuenioRepository _duenioRepository;

    public ModificarDuenioCommandHandler(IDuenioRepository duenioRepository)
    {
        _duenioRepository = duenioRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(Guid id, ModificarDuenioCommand request)
    {
        var duenio = await _duenioRepository.GetByIdAsync(id);
        if (duenio is null)
            return Error.NotFound("Owner.NotFound", "Dueño no encontrado.");

        duenio.Update(
            request.nombre_completo,
            request.direccion,
            request.telefono,
            request.correo
        );

        await _duenioRepository.UpdateAsync(duenio);
        await _duenioRepository.SaveChangesAsync();

        return duenio.Id;
    }
}
