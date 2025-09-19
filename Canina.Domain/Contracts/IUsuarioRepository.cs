using Canina.Domain.Entities;

namespace Canina.Domain.Contracts;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByUsernameAsync(string username);
    Task UpdateAsync(Usuario usuario);
}
