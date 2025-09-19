using Canina.Domain.Entities;

namespace Canina.Application.Interfaces;

public interface IJwtProvider
{
    string GenerateAccessToken(Usuario user); 
    string GenerateRefreshToken();
}
