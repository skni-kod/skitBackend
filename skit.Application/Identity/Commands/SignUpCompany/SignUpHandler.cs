using MediatR;
using skit.Application.Identity.Events.SendConfirmAccountEmail;
using skit.Core.Identity.DTO;
using skit.Core.Identity.Services;

namespace skit.Application.Identity.Commands.SignUpCompany;

public sealed class SignUpHandler : IRequestHandler<SignUpCommand, JsonWebToken>
{
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;

    public SignUpHandler(IIdentityService identityService, IMediator mediator)
    {
        _identityService = identityService;
        _mediator = mediator;
    }
    
    public async Task<JsonWebToken> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var userId = await _identityService.SignUp(request.Email, request.Password, cancellationToken);

        await _mediator.Publish(new SendConfirmAccountEmailEvent(userId), cancellationToken);

        var token = await _identityService.SignIn(request.Email, request.Password, cancellationToken);

        return token;
    }
}