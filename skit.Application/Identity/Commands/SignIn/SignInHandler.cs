﻿using MediatR;
using skit.Core.Identity.DTO;
using skit.Core.Identity.Services;

namespace skit.Application.Identity.Commands.SignIn;

public sealed class SignInHandler : IRequestHandler<SignInCommand, JsonWebToken>
{
    private readonly IIdentityService _identityService;

    public SignInHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<JsonWebToken> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var jwtWebToken = await _identityService.SignIn(request.Email, request.Password, cancellationToken);

        return jwtWebToken;
    }
}