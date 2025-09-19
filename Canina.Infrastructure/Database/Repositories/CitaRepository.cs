using Canina.Domain.Contracts;
using Canina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Canina.Infrastructure.Database.Repositories;

public class CitaRepository : ICitaRepository
{
    private readonly CaninaDbContext _context;

    public CitaRepository(CaninaDbContext context) => _context = context;

    public async Task<List<Cita>> GetByDateRangeAsync(DateTime desde, DateTime hasta)
    {
        return await _context.Citas
            .Include(c => c.Mascota).ThenInclude(m => m.Duenio)
            .Include(c => c.Servicio)
            .Include(c => c.Profesional)
            .Where(c => c.FechaHoraInicio >= desde && c.FechaHoraFin <= hasta)
            .ToListAsync();
    }

    public async Task<Cita?> GetByIdAsync(Guid id)
    {
        return await _context.Citas
            .Include(c => c.Mascota).ThenInclude(m => m.Duenio)
            .Include(c => c.Servicio)
            .Include(c => c.Profesional)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Cita cita) => await _context.Citas.AddAsync(cita);

    public Task UpdateAsync(Cita cita)
    {
        _context.Citas.Update(cita);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
