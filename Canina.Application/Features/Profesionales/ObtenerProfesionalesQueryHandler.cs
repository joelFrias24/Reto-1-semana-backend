using Canina.Domain.Contracts;
using ErrorOr;

namespace Canina.Application.Features.Profesionales;

public class ObtenerProfesionalesQueryHandler
{
    private readonly IProfesionalRepository _profesionalRepository;

    public ObtenerProfesionalesQueryHandler(IProfesionalRepository profesionalRepository)
    {
        _profesionalRepository = profesionalRepository;
    }

    public async Task<ErrorOr<IEnumerable<ObtenerProfesionalesResponse>>> Handle()
    {
        var profesionales = await _profesionalRepository.GetAllAsync();
        
        var respuesta = profesionales.Select(p => new ObtenerProfesionalesResponse(
            p.Id,
            p.NombreCompleto,
            p.Especialidad
        )).ToList();

        return respuesta;
    }
}
