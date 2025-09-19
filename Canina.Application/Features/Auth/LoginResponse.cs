namespace Canina.Application.Features.Auth;

public sealed record LoginResponse
{
    public string AccessToken { get; init; } = string.Empty;
    public UserInfo User { get; init; } = new UserInfo();
}

public record UserInfo
{
    public Guid Id { get; init; } = Guid.Empty;
    public string NombreUsuario { get; init; } = string.Empty;
}