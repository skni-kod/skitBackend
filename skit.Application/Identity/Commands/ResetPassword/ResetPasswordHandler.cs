using MediatR;
using skit.Core.Identity.Services;

namespace skit.Application.Identity.Commands.ResetPassword;

public sealed class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand>
{
    private readonly IIdentityService _identityService;

    public ResetPasswordHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        await _identityService.ResetPasswordAsync(request.UserId, request.Token, request.Password, cancellationToken);
    }
}