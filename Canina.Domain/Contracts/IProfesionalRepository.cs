using Canina.Domain.Entities;

namespace Canina.Domain.Contracts;

public interface IProfesionalRepository
{
    public Task<IEnumerable<Profesionale>> GetAllAsync();
}
