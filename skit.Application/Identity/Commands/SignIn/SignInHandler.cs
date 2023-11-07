using MediatR;
using skit.Core.Identity.DTO;

namespace skit.Application.Identity.Commands.SignIn;

public sealed class SignInHandler : IRequestHandler<SignInCommand, JwtWebToken>
{
    public Task<JwtWebToken> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}