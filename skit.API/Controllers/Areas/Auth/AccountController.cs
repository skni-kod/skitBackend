using System.Net;
using Microsoft.AspNetCore.Mvc;
using skit.API.Attributes;
using skit.API.Common;
using skit.Application.Identity.Commands.RefreshToken;
using skit.Application.Identity.Commands.SignIn;
using skit.Application.Identity.Commands.SignOut;
using skit.Application.Identity.Commands.SignUpCompany;
using skit.Application.Identity.Events.SendConfirmAccountEmail;
using skit.Core.Identity.DTO;
using skit.Core.Identity.Static;

namespace skit.API.Controllers.Areas.Auth;

[Route($"{Endpoints.BaseUrl}/account")]
public sealed class AccountController : BaseController
{
    [HttpPost("sign-up-company")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JsonWebToken>> SignUpCompany([FromBody] SignUpCompanyCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Created(string.Empty, result);
    }

    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JsonWebToken>> SignIn([FromBody] SignInCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        SetRefreshTokenCookie(result.RefreshToken);
        return Ok(result);
    }

    [HttpPost("sign-out")]
    [ApiAuthorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignOut(CancellationToken cancellationToken)
    {
        var refreshToken = Request.Cookies[Tokens.RefreshToken];
        await Mediator.Send(new SignOutCommand(refreshToken), cancellationToken);
        return Ok();
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JsonWebToken>> RefreshToken(CancellationToken cancellationToken)
    {
        var refreshToken = Request.Cookies[Tokens.RefreshToken];
        var result = await Mediator.Send(new RefreshTokenCommand(refreshToken), cancellationToken);
        SetRefreshTokenCookie(result.RefreshToken);
        return Ok(result);
    }

    [HttpPost("confirm-account")]
    [ApiAuthorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendConfirmAccountEmail(CancellationToken cancellationToken)
    {
        await Mediator.Publish(new SendConfirmAccountEmailEvent(), cancellationToken);
        return Ok();
    }
    
    private void SetRefreshTokenCookie(RefreshToken refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.Expires
        };
        Response.Cookies.Append(Tokens.RefreshToken, refreshToken.Token, cookieOptions);
    }
}