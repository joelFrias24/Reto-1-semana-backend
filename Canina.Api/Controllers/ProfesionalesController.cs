using Canina.Api.Extensions;
using Canina.Application.Features.Profesionales;
using Microsoft.AspNetCore.Mvc;

namespace Canina.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfesionalesController : ControllerBase
{
    private readonly ObtenerProfesionalesQueryHandler _obtenerProfesionalesQuery;

    public ProfesionalesController(ObtenerProfesionalesQueryHandler obtenerProfesionalesQuery)
    {
        _obtenerProfesionalesQuery = obtenerProfesionalesQuery;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var resultado = await _obtenerProfesionalesQuery.Handle();
        return resultado.Match(
            value => Ok(value),
            errors => errors.ToProblemDetails()
        );
    }
}
