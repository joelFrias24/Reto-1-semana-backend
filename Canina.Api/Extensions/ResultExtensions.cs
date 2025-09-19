using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Canina.Api.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToProblemDetails(this List<Error> errors)
    {
        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

        var problemDetails = new ProblemDetails
        {
            Type = GetType(firstError),
            Title = GetTitle(firstError),
            Status = statusCode,
            Detail = firstError.Description
        };

        // opcional: todos los errores agrupados por código
        problemDetails.Extensions["errors"] = errors
            .GroupBy(e => e.Code)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.Description).ToArray()
            );

        return new ObjectResult(problemDetails)
        {
            StatusCode = statusCode
        };
    }

    private static string GetTitle(Error error) =>
        error.Type switch
        {
            ErrorType.NotFound => "Recurso no encontrado",
            ErrorType.Validation => "Error de validación",
            ErrorType.Conflict => "Conflicto en la solicitud",
            ErrorType.Unauthorized => "No autorizado",
            _ => "Error inesperado"
        };

    private static string GetType(Error error) =>
        error.Type switch
        {
            ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
            ErrorType.Unauthorized => "https://tools.ietf.org/html/rfc7235#section-3.1",
            _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };
}
