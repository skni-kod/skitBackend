using System.Net;
using Microsoft.AspNetCore.Mvc;
using skit.API.Attributes;
using skit.API.Common;
using skit.Application.Identity.Commands.ConfirmAccount;
using skit.Application.Identity.Commands.RefreshToken;
using skit.Application.Identity.Commands.ResetPassword;
using skit.Application.Identity.Commands.SignIn;
using skit.Application.Identity.Commands.SignOut;
using skit.Application.Identity.Commands.SignUpCompany;
using skit.Application.Identity.Events.SendConfirmAccountEmail;
using skit.Application.Identity.Events.SendResetPasswordEmail;
using skit.Core.Identity.DTO;
using skit.Core.Identity.Static;

namespace skit.API.Controllers.Areas.Auth;

[Route($"{Endpoints.BaseUrl}/account")]
public sealed class AccountController : BaseController
{
    /// <summary>
    /// Sign up user and create company
    /// </summary>
    [HttpPost("sign-up-company")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JsonWebToken>> SignUpCompany([FromBody] SignUpCompanyCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Created(string.Empty, result);
    }

    /// <summary>
    /// Sign in
    /// </summary>
    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JsonWebToken>> SignIn([FromBody] SignInCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        SetRefreshTokenCookie(result.RefreshToken);
        return Ok(result);
    }

    /// <summary>
    /// Sign out
    /// </summary>
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

    /// <summary>
    /// Refresh token
    /// </summary>
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

    /// <summary>
    /// Send email with confirm account link
    /// </summary>
    [HttpPost("send-confirm-account-request")]
    [ApiAuthorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendConfirmAccountEmail(CancellationToken cancellationToken)
    {
        await Mediator.Publish(new SendConfirmAccountEmailEvent(), cancellationToken);
        return Ok();
    }
    
    /// <summary>
    /// Confirm account
    /// </summary>
    [HttpPost("confirm-account")]
    [ApiAuthorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmAccount([FromBody] ConfirmAccountCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);
        return Ok();
    }
    
    /// <summary>
    /// Send email with reset password link
    /// </summary>
    [HttpPost("send-reset-password-request")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendResetPasswordEmail([FromBody] SendResetPasswordEmailEvent @event, CancellationToken cancellationToken)
    {
        await Mediator.Publish(@event, cancellationToken);
        return Ok();
    }
    
    /// <summary>
    /// Reset password
    /// </summary>
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);
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