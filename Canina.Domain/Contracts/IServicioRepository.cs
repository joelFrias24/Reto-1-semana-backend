using Canina.Domain.Entities;

namespace Canina.Domain.Contracts;

public interface IServicioRepository
{
    public Task<IEnumerable<Servicio>> GetAllAsync();
}
