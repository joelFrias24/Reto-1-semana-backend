namespace Canina.Application.Features.Dueños.Queries;

public interface IDueniosBaseQueryResponse
{
    Guid id { get; }
    string nombre_completo { get; }
    string num_identificacion { get; }
    string? direccion { get; }
    string? telefono { get; }
    string? correo { get; }
}
