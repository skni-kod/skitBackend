using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace skit.API.Controllers;

public class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    
    protected ActionResult<TResult> OkOrNotFound<TResult>(TResult result)
    {
        return result is null ? NotFound() : Ok(result);
    }
}