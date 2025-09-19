using Canina.Domain.Contracts;
using ErrorOr;

namespace Canina.Application.Features.Servicios;

public class ObtenerServiciosQueryHandler
{
    private readonly IServicioRepository _servicioRepository;

    public ObtenerServiciosQueryHandler(IServicioRepository servicioRepository)
    {
        _servicioRepository = servicioRepository;
    }

    public async Task<ErrorOr<IEnumerable<ObtenerServiciosResponse>>> Handle()
    {
        var servicios = await _servicioRepository.GetAllAsync();
        var respuesta = servicios.Select(s => new ObtenerServiciosResponse(
            s.Id,
            s.Nombre,
            s.Descripcion
        )).ToList();

        return respuesta;
    }
}
