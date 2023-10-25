using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using skit.Shared.Abstractions.Exceptions;

namespace skit.API.Filters;

public class ExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        if (context.Exception is SkitException)
        {
            HandleNetCoreTemplateException(context);
            return;
        }

        HandleUnknownException(context);
    }
    
    private void HandleNetCoreTemplateException(ExceptionContext context)
    {
        var exception = context.Exception as SkitException;

        var details = new ProblemDetails
        {
            Title = exception?.Message
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Detail = $"{context.Exception.Message} {context.Exception.Source} {context.Exception.StackTrace}"
        };
        
        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }
}