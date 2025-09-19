using Canina.Application.Features.Auth;
using Canina.Application.Features.Citas.ChangeStatus;
using Canina.Application.Features.Citas.Crear;
using Canina.Application.Features.Citas.Modificar;
using Canina.Application.Features.Citas.ObtenerPorRangoFecha;
using Canina.Application.Features.Dueños.Crear;
using Canina.Application.Features.Dueños.Modificar;
using Canina.Application.Features.Dueños.Obtener;
using Canina.Application.Features.Dueños.ObtenerTodos;
using Canina.Application.Features.Dueños.ObtenerTodosPaginado;
using Canina.Application.Features.Mascotas.Crear;
using Canina.Application.Features.Mascotas.Modificar;
using Canina.Application.Features.Mascotas.ObtenerTodos;
using Canina.Application.Features.Mascotas.ObtenerTodosPaginado;
using Canina.Application.Features.Profesionales;
using Canina.Application.Features.Servicios;
using Microsoft.Extensions.DependencyInjection;

namespace Canina.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //services.AddMediatR(config =>
        //    config.RegisterServicesFromAssembly(typeof(ApplicationServicesRegistration).Assembly));

        services.AddScoped<LoginCommandHandler>();
        
        services.AddScoped<CrearDuenioCommandHandler>();
        services.AddScoped<ModificarDuenioCommandHandler>();
        services.AddScoped<GetAllDueniosQueryHandler>();
        services.AddScoped<GetAllDueniosPagedHandler>();
        services.AddScoped<GetDuenioByIdQueryHandler>();
        
        services.AddScoped<CrearMascotaCommandHandler>();
        services.AddScoped<ModificarMascotaCommandHandler>();
        services.AddScoped<GetAllMascotasPagedHandler>();
        services.AddScoped<ObtenerMascotasHandler>();

        services.AddScoped<CrearCitaCommandHandler>();
        services.AddScoped<ModificarCitaCommandHandler>();
        services.AddScoped<ChangeStatusCommandHandler>();
        services.AddScoped<ObtenerPorRangoFechaHandler>();

        services.AddScoped<ObtenerServiciosQueryHandler>();

        services.AddScoped<ObtenerProfesionalesQueryHandler>();

        return services;
    }
}
