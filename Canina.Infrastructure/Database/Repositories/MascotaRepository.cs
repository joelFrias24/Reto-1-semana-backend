using Canina.Domain.Contracts;
using Canina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Canina.Infrastructure.Database.Repositories;

public class MascotaRepository : IMascotaRepository
{
    private readonly CaninaDbContext _context;

    public MascotaRepository(CaninaDbContext context) => _context = context;

    public async Task<IEnumerable<Mascota>> GetAllAsync()
    {
        return await _context.Mascotas
            .Include(m => m.Duenio)
            .ToListAsync();
    }

    public async Task<(IEnumerable<Mascota> Items, int TotalCount)> GetPagedAsync(int pagina, int tamaño_pagina, string? busqueda)
    {
        var query = _context.Mascotas.AsQueryable();

        if (!string.IsNullOrEmpty(busqueda))
        {
            // Convertir la búsqueda a minúsculas para una comparación insensible a mayúsculas
            var lowerCaseSearch = busqueda.ToLower();

            query = query
                    .Include(m => m.Duenio)
                    .Where(m => m.Nombre.ToLower().Contains(lowerCaseSearch) ||
                                m.Duenio.NombreCompleto.ToLower().Contains(lowerCaseSearch));
        }
        else
        {
            query = query.Include(m => m.Duenio);
        }

        var totalCount = await query.CountAsync();
        var items = await query.Skip((pagina - 1) * tamaño_pagina)
                               .Take(tamaño_pagina)
                               .ToListAsync();

        return (items, totalCount);
    }

    public async Task<Mascota?> GetByIdAsync(Guid id)
    {
        return await _context.Mascotas
            .Include(m => m.Duenio)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<Mascota>> GetByOwnerAsync(Guid duenio_id)
    {
        return await _context.Mascotas
            .Where(m => m.DuenioId == duenio_id)
            .ToListAsync();
    }

    public async Task AddAsync(Mascota mascota) => await _context.Mascotas.AddAsync(mascota);

    public Task UpdateAsync(Mascota mascota)
    {
        _context.Mascotas.Update(mascota);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
