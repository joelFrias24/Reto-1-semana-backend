using System.Xml.Linq;

namespace Canina.Domain.Entities;

public partial class Servicio
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    private Servicio() { }

    public Servicio(string name, string? description)
    {
        Id = Guid.NewGuid();
        Nombre = name;
        Descripcion = description;
    }
}
