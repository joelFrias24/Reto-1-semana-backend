using Canina.Domain.Entities;

namespace Canina.Domain.Contracts;

public interface IDuenioRepository
{
    Task<Duenio?> GetByIdAsync(Guid id);
    Task<(IEnumerable<Duenio> Items, int TotalCount)> GetPagedAsync(int pagina, int tamaño_pagina);
    Task<Duenio?> GetByIdentificationAsync(string identificacion);
    Task<List<Duenio>> GetAllAsync();
    Task AddAsync(Duenio duenio);
    Task UpdateAsync(Duenio duenio);
    Task SaveChangesAsync();
}
