using Canina.Domain.Contracts;
using Canina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Canina.Infrastructure.Database.Repositories;

public class ProfesionalRepository : IProfesionalRepository
{
    private readonly CaninaDbContext _context;

    public ProfesionalRepository(CaninaDbContext context) => _context = context;

    public async Task<IEnumerable<Profesionale>> GetAllAsync()
    {
        return await _context.Profesionales
            .ToListAsync();
    }
}
