using Canina.Domain.Contracts;
using ErrorOr;

namespace Canina.Application.Features.Dueños.Obtener;

public class GetDuenioByIdQueryHandler
{
    private readonly IDuenioRepository _duenioRepository;

    public GetDuenioByIdQueryHandler(IDuenioRepository duenioRepository)
    {
        _duenioRepository = duenioRepository;
    }

    public async Task<ErrorOr<GetDuenioByIdResponse>> Handle(Guid id)
    {
        var duenio = await _duenioRepository.GetByIdAsync(id);
        if (duenio is null)
            return Error.NotFound("Owner.NotFound", "Dueño no encontrado");

        return new GetDuenioByIdResponse(
            duenio.Id, 
            duenio.NombreCompleto, 
            duenio.NumIdentificacion,
            duenio.Direccion, 
            duenio.Telefono, 
            duenio.Correo
        );
    }
}
