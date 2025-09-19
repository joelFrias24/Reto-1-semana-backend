using Canina.Domain.Entities;

namespace Canina.Domain.Contracts;

public interface IMascotaRepository
{
    Task<IEnumerable<Mascota>> GetAllAsync();
    Task<(IEnumerable<Mascota> Items, int TotalCount)> GetPagedAsync(int pagina, int tamaño_pagina, string busqueda);
    Task<Mascota?> GetByIdAsync(Guid id);
    Task<List<Mascota>> GetByOwnerAsync(Guid duenio_id);
    Task AddAsync(Mascota mascota);
    Task UpdateAsync(Mascota mascota);
    Task SaveChangesAsync();
}