using Canina.Domain.Contracts;
using ErrorOr;

namespace Canina.Application.Features.Mascotas.ObtenerTodos;

public class ObtenerMascotasHandler
{
    private readonly IMascotaRepository _mascotaRepository;

    public ObtenerMascotasHandler(IMascotaRepository mascotaRepository)
    {
        _mascotaRepository = mascotaRepository;
    }

    public async Task<ErrorOr<IEnumerable<ObtenerMascotasResponse>>> Handle()
    {
        var mascotas = await _mascotaRepository.GetAllAsync();
        var respuesta = mascotas.Select(m => new ObtenerMascotasResponse(
            m.Id,
            m.Nombre,
            m.Especie,
            m.Raza,
            m.FechaNacimiento,
            m.Sexo,
            m.Color,
            m.Peso,
            m.Notas,
            m.DuenioId.ToString(),
            m.Duenio.NombreCompleto
        )).ToList();
        
        return respuesta;
    }
}
