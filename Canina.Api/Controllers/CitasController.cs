using Canina.Api.Extensions;
using Canina.Application.Features.Citas.ChangeStatus;
using Canina.Application.Features.Citas.Crear;
using Canina.Application.Features.Citas.Modificar;
using Canina.Application.Features.Citas.ObtenerPorRangoFecha;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Canina.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CitasController : ControllerBase
{
    private readonly CrearCitaCommandHandler _crearHandler;
    private readonly ModificarCitaCommandHandler _modificarHandler;
    private readonly ChangeStatusCommandHandler _changeStatusHandler;
    private readonly ObtenerPorRangoFechaHandler _obtenerPorRangoHandler;

    public CitasController(
        CrearCitaCommandHandler crearHandler, 
        ModificarCitaCommandHandler modificarHandler, 
        ChangeStatusCommandHandler changeStatusHandler,
        ObtenerPorRangoFechaHandler obtenerPorRangoHandler)
    {
        _crearHandler = crearHandler;
        _modificarHandler = modificarHandler;
        _changeStatusHandler = changeStatusHandler;
        _obtenerPorRangoHandler = obtenerPorRangoHandler;
    }

    [HttpGet("week")]
    public async Task<IActionResult> GetAppointmentsForWeek([FromQuery] DateTime fecha_inicio)
    {
        var result = await _obtenerPorRangoHandler.Handle(fecha_inicio);

        return result.Match(
            value => Ok(value),
            errors => errors.ToProblemDetails()
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CrearCitaCommand command)
    {
        ErrorOr<Guid> resultado = await _crearHandler.Handle(command);

        return resultado.Match(
            value => Ok(value),
            errors => errors.ToProblemDetails()
        );
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ModificarCitaCommand command)
    {
        ErrorOr<Guid> resultado = await _modificarHandler.Handle(id, command);

        return resultado.Match(
            value => Ok(value),
            errors => errors.ToProblemDetails()
        );
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] ChangeStatusCommand command)
    {
        ErrorOr<ChangeStatusResponse> resultado = await _changeStatusHandler.Handle(command);

        return resultado.Match(
            value => Ok(value),
            errors => errors.ToProblemDetails()
        );
    }

}
