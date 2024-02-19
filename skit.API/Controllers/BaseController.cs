using MediatR;
using Microsoft.AspNetCore.Mvc;
using skit.API.Common;
using skit.Core.Identity.DTO;

namespace skit.API.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    
    protected ActionResult<TResult> OkOrNotFound<TResult>(TResult result)
    {
        return result is null ? NotFound() : Ok(result);
    }
    
    protected void SetRefreshTokenCookie(RefreshToken refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.Expires
        };
        Response.Cookies.Append(Tokens.RefreshToken, refreshToken.Token, cookieOptions);
    }
}