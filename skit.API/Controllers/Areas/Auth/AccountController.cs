using Microsoft.AspNetCore.Mvc;
using skit.Application.Identity.Commands.SignUpCompany;

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
}