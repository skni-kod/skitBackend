using MediatR;
using skit.Core.Identity.Services;

namespace skit.Application.Identity.Commands.SignOut;

public sealed class SignOutHandler : IRequestHandler<SignOutCommand>
{
    private readonly IIdentityService _identityService;

    public SignOutHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        await _identityService.SignOut(request.RefreshToken,cancellationToken);
    }
}