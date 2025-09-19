namespace Canina.Application.Features.Dueños.Modificar;

public record ModificarDuenioCommand(
    string nombre_completo,
    string num_identificacion,
    string? direccion,
    string? telefono,
    string? correo
);