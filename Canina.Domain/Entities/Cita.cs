namespace Canina.Domain.Entities;

public partial class Cita
{
    public Guid Id { get; private set; }
    public Guid MascotaId { get; private set; }
    public Guid ServicioId { get; private set; }
    public Guid? ProfesionalId { get; private set; }
    public DateTime FechaHoraInicio { get; private set; }
    public DateTime FechaHoraFin { get; private set; }
    public string? Motivo { get; private set; }
    public EstatusCita Estatus { get; private set; }
    public string? Observaciones { get; private set; }
    public virtual ICollection<DetallesServiciosCita> DetallesServiciosCita { get; set; } = new List<DetallesServiciosCita>();
    
    public virtual Mascota Mascota { get; set; } = null!;
    public virtual Profesionale? Profesional { get; set; }
    public virtual Servicio Servicio { get; set; } = null!;

    private Cita() { }

    public Cita(Guid mascota_id, Guid servicio_id, Guid? profesional_id,
        DateTime fecha_inicio, DateTime fecha_fin, string? motivo, string? observaciones)
    {
        Id = Guid.NewGuid();
        MascotaId = mascota_id;
        ServicioId = servicio_id;
        ProfesionalId = profesional_id;
        FechaHoraInicio = fecha_inicio;
        FechaHoraFin = fecha_fin;
        Motivo = motivo;
        Estatus = EstatusCita.Pendiente;
        Observaciones = observaciones;
    }

    public void Update(Guid servicio_id, Guid? profesinal_id,
        DateTime fecha_hora_inicio, DateTime fecha_hora_fin, string? motivo, string? observaciones)
    {
        ServicioId = servicio_id;
        ProfesionalId = profesinal_id;
        FechaHoraInicio = fecha_hora_inicio;
        FechaHoraFin = fecha_hora_fin;
        Motivo = motivo;
        Observaciones = observaciones;
    }

    public void ChangeStatus(EstatusCita nuevo_estatus)
    {
        Estatus = nuevo_estatus;
    }
}

public enum EstatusCita
{
    Pendiente,
    Confirmada,
    Realizada,
    Cancelada
}
