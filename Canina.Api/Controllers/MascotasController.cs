using Canina.Api.Extensions;
using Canina.Application;
using Canina.Application.Features.Mascotas.Crear;
using Canina.Application.Features.Mascotas.Modificar;
using Canina.Application.Features.Mascotas.ObtenerTodos;
using Canina.Application.Features.Mascotas.ObtenerTodosPaginado;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Canina.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MascotasController : ControllerBase
{
    private readonly CrearMascotaCommandHandler _crearHandler;
    private readonly ModificarMascotaCommandHandler _modificarHandler;
    private readonly GetAllMascotasPagedHandler _getAllMascotas;
    private readonly ObtenerMascotasHandler _obtenerMascotas;

    public MascotasController(CrearMascotaCommandHandler crearHandler, ModificarMascotaCommandHandler modificarHandler, GetAllMascotasPagedHandler getAllMascotas, ObtenerMascotasHandler obtenerMascotas)
    {
        _crearHandler = crearHandler;
        _modificarHandler = modificarHandler;
        _getAllMascotas = getAllMascotas;
        _obtenerMascotas = obtenerMascotas;
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetAllPaged(int page = 1, int pageSize = 10, string? search = null)
    {
        ErrorOr<PagedResultDto<GetAllMascotasPagedResponse>> resultado = 
            await _getAllMascotas.Handle(page, pageSize, search);

        return resultado.Match(
            value => Ok(value),
            errors => errors.ToProblemDetails()
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var resultado = await _obtenerMascotas.Handle();

        return resultado.Match(
            value => Ok(value),
            errors => errors.ToProblemDetails()
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CrearMascotaCommand command)
    {
        ErrorOr<Guid> result = await _crearHandler.Handle(command);
        return result.Match(
            value => Ok(value),
            errors => errors.ToProblemDetails()
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ModificarMascotaCommand command)
    {
        ErrorOr<Guid> result = await _modificarHandler.Handle(id, command);

        return result.Match(
            value => Ok(value),
            errors => errors.ToProblemDetails()
        );
    }
}
