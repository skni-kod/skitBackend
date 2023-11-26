﻿using System.Security.Claims;
using skit.Core.Identity.DTO;

namespace skit.Core.Identity.Services;

public interface ITokenService
{
    Task<JwtWebToken> GenerateAccessToken(Guid userId, string userEmail, ICollection<string> roles, ICollection<Claim> claims);
}