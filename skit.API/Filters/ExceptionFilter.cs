using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using skit.Core.Identity.Exceptions;
using skit.Shared.Abstractions.Exceptions;

namespace skit.API.Filters;

public class ExceptionFilter : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
    
    public ExceptionFilter()
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(CreateUserException), HandleCreateUserException },
            { typeof(ChangePasswordException), HandleChangePasswordException },
        };
    }
    
    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }
        
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
    
    private void HandleCreateUserException(ExceptionContext context)
    {
        var exception = context.Exception as CreateUserException;
        
        var details = new ValidationProblemDetails(exception?.Errors)
        {
            Title = exception?.Message
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleChangePasswordException(ExceptionContext context)
    {
        var exception = context.Exception as ChangePasswordException;
        
        var details = new ValidationProblemDetails(exception?.Errors)
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