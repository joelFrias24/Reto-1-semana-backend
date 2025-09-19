using Canina.Application.Features.Mascotas.ObtenerTodosPaginado;
using Canina.Domain.Contracts;
using ErrorOr;

namespace Canina.Application.Features.Dueños.ObtenerTodosPaginado;

public class GetAllDueniosPagedHandler
{
    private readonly IDuenioRepository _duenioRepository;

    public GetAllDueniosPagedHandler(IDuenioRepository duenioRepository)
    {
        _duenioRepository = duenioRepository;
    }

    public async Task<ErrorOr<PagedResultDto<GetAllDueniosPagedResponse>>> Handle(int pagina, int tamaño_pagina)
    {
        var (duenios, totalCount) = await _duenioRepository.GetPagedAsync(pagina, tamaño_pagina);

        var items = duenios.Select(d => new GetAllDueniosPagedResponse(
            d.Id,
            d.NombreCompleto,
            d.NumIdentificacion,
            d.Direccion,
            d.Telefono,
            d.Correo,
            d.Mascota.Select(m => new MascotaResponse(
                m.Id,
                m.Nombre
            )).ToList()
        )).ToList();

        var pagedResult = new PagedResultDto<GetAllDueniosPagedResponse>
        {
            Items = items,
            TotalCount = totalCount,
            Page = pagina,
            PageSize = tamaño_pagina
        };

        return pagedResult;
    }
}
