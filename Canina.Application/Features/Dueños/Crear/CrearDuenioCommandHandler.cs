using Canina.Domain.Contracts;
using Canina.Domain.Entities;
using ErrorOr;

namespace Canina.Application.Features.Dueños.Crear;

public class CrearDuenioCommandHandler
{
    private readonly IDuenioRepository _duenioRepository;

    public CrearDuenioCommandHandler(IDuenioRepository duenioRepository)
    {
        _duenioRepository = duenioRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CrearDuenioCommand request)
    {
        var usuario_existente = await _duenioRepository.GetByIdentificationAsync(request.num_identificacion);
        if (usuario_existente is not null)
            return Error.Conflict("Owner.Duplicate", "Ya existe un dueño con esa identificación.");

        var duenio = new Duenio(
            request.nombre_completo,
            request.num_identificacion,
            request.direccion,
            request.telefono,
            request.correo
        );

        await _duenioRepository.AddAsync(duenio);
        await _duenioRepository.SaveChangesAsync();

        return duenio.Id;
    }
}
