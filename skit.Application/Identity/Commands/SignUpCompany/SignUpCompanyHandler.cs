using MediatR;
using skit.Core.Identity.DTO;
using skit.Core.Identity.Services;

namespace skit.Application.Identity.Commands.SignUpCompany;

public sealed class SignUpCompanyHandler : IRequestHandler<SignUpCompanyCommand, JwtWebToken>
{
    private readonly IIdentityService _identityService;

    public SignUpCompanyHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<JwtWebToken> Handle(SignUpCompanyCommand request, CancellationToken cancellationToken)
    {
        await _identityService.SignUpCompany(request.Email, request.CompanyName, request.Password, cancellationToken);

        var token = await _identityService.SignIn(request.Email, request.Password, cancellationToken);

        return token;
    }
}