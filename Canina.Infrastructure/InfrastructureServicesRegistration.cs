using Canina.Application.Interfaces;
using Canina.Domain.Contracts;
using Canina.Infrastructure.Database;
using Canina.Infrastructure.Database.Repositories;
using Canina.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Canina.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CaninaDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IDuenioRepository, DuenioRepository>();
        services.AddScoped<IMascotaRepository, MascotaRepository>();
        services.AddScoped<ICitaRepository, CitaRepository>();
        services.AddScoped<IServicioRepository, ServicioRepository>();

        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddScoped<IProfesionalRepository, ProfesionalRepository>();
        //services.AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
