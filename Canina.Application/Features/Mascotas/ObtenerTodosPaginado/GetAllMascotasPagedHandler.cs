using Canina.Domain.Contracts;
using ErrorOr;

namespace Canina.Application.Features.Mascotas.ObtenerTodosPaginado;

public class GetAllMascotasPagedHandler
{
    private readonly IMascotaRepository _mascotaRepository;

    public GetAllMascotasPagedHandler(IMascotaRepository mascotaRepository)
    {
        _mascotaRepository = mascotaRepository;
    }

    public async Task<ErrorOr<PagedResultDto<GetAllMascotasPagedResponse>>> Handle(int pagina, int tamaño_pagina, string? busqueda)
    {
        var (mascotas, totalCount) = await _mascotaRepository.GetPagedAsync(pagina, tamaño_pagina, busqueda);
        
        var items = mascotas.Select(m => new GetAllMascotasPagedResponse(
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

        var pagedResult = new PagedResultDto<GetAllMascotasPagedResponse>
        {
            Items = items,
            TotalCount = totalCount,
            Page = pagina,
            PageSize = tamaño_pagina
        };

        return pagedResult;
    }
}
