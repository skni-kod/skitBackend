using MediatR;
using skit.Core.Identity.Services;

namespace skit.Application.Identity.Commands.ConfirmAccount;

public sealed class ConfirmAccountHandler : IRequestHandler<ConfirmAccountCommand>
{
    private readonly IIdentityService _identityService;

    public ConfirmAccountHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task Handle(ConfirmAccountCommand request, CancellationToken cancellationToken)
    {
        await _identityService.ConfirmAccountAsync(request.UserId, request.Token, cancellationToken);
    }
}