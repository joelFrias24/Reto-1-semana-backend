using System.Xml.Linq;

namespace Canina.Domain.Entities;

public partial class Mascota
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Especie { get; set; } = null!;
    public string Raza { get; set; } = null!;
    public DateOnly? FechaNacimiento { get; set; }
    public string Sexo { get; set; } = null!;
    public string? Color { get; set; }
    public decimal? Peso { get; set; }
    public string? Notas { get; set; }
    public Guid DuenioId { get; set; }

    public virtual Duenio Duenio { get; set; } = null!;
    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    private Mascota() { }

    public Mascota(string nombre, string especie, string raza, DateOnly? fecha_nacimiento, string sexo,
        string? color, decimal? peso, string? notas, Guid duenio_id)
    {
        Id = Guid.NewGuid();
        Nombre = nombre;
        Especie = especie;
        Raza = raza;
        FechaNacimiento = fecha_nacimiento;
        Sexo = sexo;
        Color = color;
        Peso = peso;
        Notas = notas;
        DuenioId = duenio_id;
    }

    public void Update(string nombre, string especie, string raza, DateOnly? fecha_nacimiento, string sexo,
        string? color, decimal? peso, string? notas, Guid duenio_id)
    {
        Nombre = nombre;
        Especie = especie;
        Raza = raza;
        FechaNacimiento = fecha_nacimiento;
        Sexo = sexo;
        Color = color;
        Peso = peso;
        Notas = notas;
        DuenioId = duenio_id;
    }
}
