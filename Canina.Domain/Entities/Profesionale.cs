namespace Canina.Domain.Entities;

public partial class Profesionale
{
    public Guid Id { get; set; }
    public string NombreCompleto { get; set; } = null!;
    public string? Especialidad { get; set; }
    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    private Profesionale() { } // EF

    public Profesionale(string fullName, string? specialty)
    {
        Id = Guid.NewGuid();
        NombreCompleto = fullName;
        Especialidad = specialty;
    }
}
