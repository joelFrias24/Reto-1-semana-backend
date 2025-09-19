using Canina.Api.Extensions;
using Canina.Application;
using Canina.Application.Features.Dueños.Crear;
using Canina.Application.Features.Dueños.Modificar;
using Canina.Application.Features.Dueños.Obtener;
using Canina.Application.Features.Dueños.ObtenerTodos;
using Canina.Application.Features.Dueños.ObtenerTodosPaginado;
using Canina.Application.Features.Mascotas.ObtenerTodosPaginado;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Canina.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DueniosController : ControllerBase
{
    private readonly CrearDuenioCommandHandler _crearHandler;
    private readonly ModificarDuenioCommandHandler _modificarHandler;
    private readonly GetAllDueniosQueryHandler _getAllHandler;
    private readonly GetDuenioByIdQueryHandler _getHandler;
    private readonly GetAllDueniosPagedHandler _getAllDueniosPaged;

    public DueniosController(
        CrearDuenioCommandHandler crearHandler, 
        ModificarDuenioCommandHandler modificarHandler,
        GetAllDueniosQueryHandler getAllHandler, 
        GetDuenioByIdQueryHandler getDuenioHandler,
        GetAllDueniosPagedHandler getAllDueniosPaged)
    {
        _crearHandler = crearHandler;
        _modificarHandler = modificarHandler;
        _getAllHandler = getAllHandler;
        _getHandler = getDuenioHandler;
        _getAllDueniosPaged = getAllDueniosPaged;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var resultado = await _getAllHandler.Handle();
        return resultado.Match(
            value => Ok(value),
            errors => errors.ToProblemDetails()
        );
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetAllPaged(int page = 1, int pageSize = 10)
    {
        var resultado = await _getAllDueniosPaged.Handle(page, pageSize);

        return resultado.Match(
            value => Ok(value),
            errors => errors.ToProblemDetails()
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var resultado = await _getHandler.Handle(id);
        return resultado.Match(
            value => Ok(value),
            errors => errors.ToProblemDetails()
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CrearDuenioCommand command)
    {
        ErrorOr<Guid> resultado = await _crearHandler.Handle(command);
        return resultado.Match(
            value => Ok(value),
            errors => errors.ToProblemDetails()
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ModificarDuenioCommand command)
    {
        ErrorOr<Guid> result = await _modificarHandler.Handle(id, command);

        return result.Match(
            value => Ok(value),
            errors => errors.ToProblemDetails()
        );
    }
}
