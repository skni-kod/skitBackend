using Microsoft.AspNetCore.Mvc;
using skit.Application.Identity.Commands.SignIn;
using skit.Application.Identity.Commands.SignUpCompany;
using skit.Core.Identity.DTO;

namespace skit.API.Controllers.Areas.Auth;

[Route($"{Endpoints.BaseUrl}/account")]
public sealed class AccountController : BaseController
{
    [HttpPost("sign-up-company")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JwtWebToken>> SignUpCompany([FromBody] SignUpCompanyCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Created(string.Empty, result);
    }

    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JwtWebToken>> SignIn([FromBody] SignInCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}