namespace Canina.Domain.Entities;

public partial class DetallesServiciosCita
{
    public Guid Id { get; set; }

    public Guid CitaId { get; set; }

    public string? Medicacion { get; set; }

    public string? Producto { get; set; }

    public string? Notas { get; set; }

    public virtual Cita Cita { get; set; } = null!;
}
