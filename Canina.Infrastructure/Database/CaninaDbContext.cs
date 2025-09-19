using Canina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Canina.Infrastructure.Database;

public partial class CaninaDbContext : DbContext
{
    public CaninaDbContext() { }

    public CaninaDbContext(DbContextOptions<CaninaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cita> Citas { get; set; }
    public virtual DbSet<DetallesServiciosCita> DetallesServiciosCitas { get; set; }
    public virtual DbSet<Duenio> Duenios { get; set; }
    public virtual DbSet<Mascota> Mascotas { get; set; }
    public virtual DbSet<Profesionale> Profesionales { get; set; }
    public virtual DbSet<Servicio> Servicios { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Citas__3213E83F882A7E8C");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Estatus)
                .HasMaxLength(20)
                .HasConversion<string>()
                .HasDefaultValue(EstatusCita.Pendiente)
                .HasColumnName("estatus");
            entity.Property(e => e.FechaHoraFin)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_fin");
            entity.Property(e => e.FechaHoraInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_inicio");
            entity.Property(e => e.MascotaId).HasColumnName("mascota_id");
            entity.Property(e => e.Motivo)
                .HasMaxLength(200)
                .HasColumnName("motivo");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(300)
                .HasColumnName("observations");
            entity.Property(e => e.ProfesionalId).HasColumnName("profesional_id");
            entity.Property(e => e.ServicioId).HasColumnName("servicio_id");

            entity.HasOne(d => d.Mascota).WithMany(p => p.Cita)
                .HasForeignKey(d => d.MascotaId)
                .HasConstraintName("FK_Citas_Mascotas");

            entity.HasOne(d => d.Profesional).WithMany(p => p.Cita)
                .HasForeignKey(d => d.ProfesionalId)
                .HasConstraintName("FK_Citas_Profesionales");

            entity.HasOne(d => d.Servicio).WithMany(p => p.Cita)
                .HasForeignKey(d => d.ServicioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Citas_Servicios");
        });

        modelBuilder.Entity<DetallesServiciosCita>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Detalles__3213E83F400551B5");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.CitaId).HasColumnName("cita_id");
            entity.Property(e => e.Medicacion)
                .HasMaxLength(100)
                .HasColumnName("medicacion");
            entity.Property(e => e.Notas)
                .HasMaxLength(300)
                .HasColumnName("notas");
            entity.Property(e => e.Producto)
                .HasMaxLength(100)
                .HasColumnName("producto");

            entity.HasOne(d => d.Cita).WithMany(p => p.DetallesServiciosCita)
                .HasForeignKey(d => d.CitaId)
                .HasConstraintName("FK_DetallesServiciosCitas_Citas");
        });

        modelBuilder.Entity<Duenio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Duenios__3213E83F5B89870B");

            entity.HasIndex(e => e.NumIdentificacion, "UQ__Duenios__4036208E3847786A").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .HasColumnName("nombre_completo");
            entity.Property(e => e.NumIdentificacion)
                .HasMaxLength(20)
                .HasColumnName("num_identificacion");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Mascota>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Mascotas__3213E83F1792CA00");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(30)
                .HasColumnName("color");
            entity.Property(e => e.DuenioId).HasColumnName("duenio_id");
            entity.Property(e => e.Especie)
                .HasMaxLength(50)
                .HasColumnName("especie");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Notas)
                .HasMaxLength(300)
                .HasColumnName("notas");
            entity.Property(e => e.Peso)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("peso");
            entity.Property(e => e.Raza)
                .HasMaxLength(50)
                .HasColumnName("raza");
            entity.Property(e => e.Sexo)
                .HasMaxLength(10)
                .HasColumnName("sexo");

            entity.HasOne(d => d.Duenio).WithMany(p => p.Mascota)
                .HasForeignKey(d => d.DuenioId)
                .HasConstraintName("FK_Mascotas_Duenios");
        });

        modelBuilder.Entity<Profesionale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Profesio__3213E83F8E7DAFA3");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Especialidad)
                .HasMaxLength(100)
                .HasColumnName("especialidad");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .HasColumnName("nombre_completo");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Servicio__3213E83F00824DC9");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3213E83FA9F0CEEE");

            entity.HasIndex(e => e.NombreUsuario, "UQ__Usuarios__D4D22D74082DA9F5").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.BloqueadoHasta)
                .HasColumnType("datetime")
                .HasColumnName("bloqueado_hasta");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(255)
                .HasColumnName("contrasenia");
            entity.Property(e => e.IntentosFallidosLogin).HasColumnName("intentos_fallidos_login");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .HasColumnName("nombre_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
