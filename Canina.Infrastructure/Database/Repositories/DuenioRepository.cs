using Canina.Domain.Contracts;
using Canina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Canina.Infrastructure.Database.Repositories;

public class DuenioRepository : IDuenioRepository
{
    private readonly CaninaDbContext _context;

    public DuenioRepository(CaninaDbContext context) => _context = context;
    
    public async Task<List<Duenio>> GetAllAsync()
    {
        return await _context.Duenios
            .Include(d => d.Mascota)
            .ToListAsync();
    }

    public async Task<(IEnumerable<Duenio> Items, int TotalCount)> GetPagedAsync(int pagina, int tamaño_pagina)
    {
        var totalCount = await _context.Duenios.CountAsync();
        
        var items = await _context.Duenios
            .Include(d => d.Mascota)
            .Skip((pagina - 1) * tamaño_pagina)
            .Take(tamaño_pagina)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<Duenio?> GetByIdAsync(Guid id)
    {
        return await _context.Duenios
            .FindAsync(id);
    }

    public async Task<Duenio?> GetByIdentificationAsync(string identificacion)
    {
        return await _context.Duenios
            .FirstOrDefaultAsync(d => d.NumIdentificacion == identificacion);
    }

    public async Task AddAsync(Duenio duenio) => await _context.Duenios.AddAsync(duenio);

    public Task UpdateAsync(Duenio duenio)
    {
        _context.Duenios.Update(duenio);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
