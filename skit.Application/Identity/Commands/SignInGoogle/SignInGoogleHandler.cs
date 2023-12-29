using MediatR;
using skit.Core.Identity.DTO;
using skit.Core.Identity.Services;

namespace skit.Application.Identity.Commands.SignInGoogle;

public sealed class SignInGoogleHandler : IRequestHandler<SignInGoogleCommand, JsonWebToken>
{
    private readonly IIdentityService _identityService;

    public SignInGoogleHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<JsonWebToken> Handle(SignInGoogleCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.GoogleAuthAsync(cancellationToken);
    }
}