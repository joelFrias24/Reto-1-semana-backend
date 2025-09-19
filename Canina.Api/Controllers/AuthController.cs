using Canina.Api.Extensions;
using Canina.Application.Features.Auth;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Canina.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly LoginCommandHandler _loginHandler;

    public AuthController(LoginCommandHandler loginHandler)
    {
        _loginHandler = loginHandler;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        ErrorOr<LoginResponse> result = await _loginHandler.Handle(command);

        return result.Match(
            value => Ok(value),
            errors => errors.ToProblemDetails()
        );
    }
}