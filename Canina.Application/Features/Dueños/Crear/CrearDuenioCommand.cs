namespace Canina.Application.Features.Dueños.Crear;

public record CrearDuenioCommand(
    string nombre_completo,
    string num_identificacion,
    string? direccion,
    string? telefono,
    string? correo
);
