namespace Canina.Domain.Entities;

public partial class Usuario
{
    public Guid Id { get; set; }
    public string NombreUsuario { get; set; } = null!;
    public string Contrasenia { get; set; } = null!;
    public int IntentosFallidosLogin { get; set; }
    public DateTime? BloqueadoHasta { get; set; }

    public Usuario() { }

    public void RegistrarIntentoFallido()
    {
        IntentosFallidosLogin++;
        if (IntentosFallidosLogin >= 3)
        {
            BloqueadoHasta = DateTime.UtcNow.AddMinutes(15);
        }
    }

    public void ReiniciarIntentosFallidos()
    {
        IntentosFallidosLogin = 0;
        BloqueadoHasta = null;
    }

    public bool estaBloqueado() => BloqueadoHasta != null && BloqueadoHasta > DateTime.UtcNow;
}
