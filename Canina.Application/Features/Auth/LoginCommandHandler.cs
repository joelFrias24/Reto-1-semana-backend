using Canina.Application.Interfaces;
using Canina.Domain.Contracts;
using ErrorOr;

namespace Canina.Application.Features.Auth;

public class LoginCommandHandler
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(IUsuarioRepository usuarioRepository, IJwtProvider jwtProvider)
    {
        _usuarioRepository = usuarioRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<ErrorOr<LoginResponse>> Handle(LoginCommand request)
    {
        var usuario = await _usuarioRepository.GetByUsernameAsync(request.username);

        if (usuario is null)
            return Error.NotFound(code: "User.NotFound", description: "Usuario no encontrado.");

        if (usuario.estaBloqueado())
            return Error.Conflict(code: "User.Locked", description: $"Usuario bloqueado hasta {usuario.BloqueadoHasta}");

        if (request.password != usuario.Contrasenia)
        {
            usuario.RegistrarIntentoFallido();
            await _usuarioRepository.UpdateAsync(usuario);
            return Error.Validation(code: "User.InvalidPassword", description: "Contraseña incorrecta.");
        }

        usuario.ReiniciarIntentosFallidos();
        await _usuarioRepository.UpdateAsync(usuario);

        var token = _jwtProvider.GenerateAccessToken(usuario);
        return new LoginResponse
        {
            AccessToken = token,
            User = new UserInfo
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario
            }
        };
    }
}
