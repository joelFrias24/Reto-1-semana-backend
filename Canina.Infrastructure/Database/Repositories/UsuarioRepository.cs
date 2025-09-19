using Canina.Domain.Contracts;
using Canina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Canina.Infrastructure.Database.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly CaninaDbContext _context;

    public UsuarioRepository(CaninaDbContext context) => _context = context;
    
    public async Task<Usuario?> GetByUsernameAsync(string username)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.NombreUsuario == username);
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }
}
