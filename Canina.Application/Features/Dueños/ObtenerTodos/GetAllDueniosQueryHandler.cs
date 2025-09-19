using Canina.Domain.Contracts;
using ErrorOr;

namespace Canina.Application.Features.Dueños.ObtenerTodos;

public class GetAllDueniosQueryHandler
{
    private readonly IDuenioRepository _duenioRepository;

    public GetAllDueniosQueryHandler(IDuenioRepository duenioRepository)
    {
        _duenioRepository = duenioRepository;
    }

    public async Task<ErrorOr<List<GetAllDueniosResponse>>> Handle()
    {
        var duenios = await _duenioRepository.GetAllAsync();
        var respuesta = duenios.Select(o => new GetAllDueniosResponse(
            o.Id, 
            o.NombreCompleto, 
            o.NumIdentificacion, 
            o.Correo, 
            o.Telefono, 
            o.Correo)
        ).ToList();

        return respuesta;
    }
}
