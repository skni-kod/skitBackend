using Microsoft.AspNetCore.Mvc;
using skit.Application.Identity.Commands.SignIn;
using skit.Application.Identity.Commands.SignUpCompany;
using skit.Core.Identity.DTO;

namespace skit.API.Controllers.Areas.Auth;

[Route("account")]
public sealed class AccountController : BaseController
{
    [HttpPost("sign-up-company")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUpCompany([FromBody] SignUpCompanyCommand command,
        CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);
        return Created(string.Empty, null);
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