using Canina.Domain.Entities;

namespace Canina.Domain.Contracts;

public interface ICitaRepository
{
    Task<Cita?> GetByIdAsync(Guid id);
    Task<List<Cita>> GetByDateRangeAsync(DateTime desde, DateTime hasta);
    Task AddAsync(Cita cita);
    Task UpdateAsync(Cita cita);
    Task SaveChangesAsync();
}
