using System.Net;
using System.Numerics;

namespace Canina.Domain.Entities;

public partial class Duenio
{
    public Guid Id { get; set; }
    public string NombreCompleto { get; set; } = null!;
    public string NumIdentificacion { get; set; } = null!;
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
    public virtual ICollection<Mascota> Mascota { get; set; } = new List<Mascota>();

    public Duenio() { }

    public Duenio(string nombre_completo, string num_identificacion, string? direccion, string? telefono, string? correo)
    {
        Id = Guid.NewGuid();
        NombreCompleto = nombre_completo;
        NumIdentificacion = num_identificacion;
        Direccion = direccion;
        Telefono = telefono;
        Correo = correo;
    }

    public void Update(string nombre_completo, string? direccion, string? telefono, string? correo)
    {
        NombreCompleto = nombre_completo;
        Direccion = direccion;
        Telefono = telefono;
        Correo = correo;
    }
}
