using Canina.Domain.Contracts;
using Canina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Canina.Infrastructure.Database.Repositories;

public class ServicioRepository : IServicioRepository
{
    private readonly CaninaDbContext _context;
    
    public ServicioRepository(CaninaDbContext context) => _context = context;
    
    public async Task<IEnumerable<Servicio>> GetAllAsync()
    {
        return await _context.Servicios
            .ToListAsync();
    }
}