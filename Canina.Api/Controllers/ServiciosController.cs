using Canina.Api.Extensions;
using Canina.Application.Features.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Canina.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiciosController : ControllerBase
{
    private readonly ObtenerServiciosQueryHandler _obtenerServiciosQuery;

    public ServiciosController(ObtenerServiciosQueryHandler obtenerServiciosQuery)
    {
        _obtenerServiciosQuery = obtenerServiciosQuery;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var resultado = await _obtenerServiciosQuery.Handle();
        return resultado.Match(
            value => Ok(value),
            errors => errors.ToProblemDetails()
        );
    }
}
