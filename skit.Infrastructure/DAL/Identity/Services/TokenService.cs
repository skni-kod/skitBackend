using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using skit.Core.Common.Services;
using skit.Core.Identity.DTO;
using skit.Core.Identity.Services;
using skit.Shared.Configurations.Identity;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace skit.Infrastructure.DAL.Identity.Services;

public sealed class TokenService : ITokenService
{
    private readonly IDateService _dateService;
    private readonly AuthConfig _authConfig;
    private readonly SigningCredentials _signingCredentials;

    public TokenService(IDateService dateService, AuthConfig authConfig)
    {
        _dateService = dateService;
        _authConfig = authConfig;
        _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.JwtKey)),
            SecurityAlgorithms.HmacSha256);
    }
    
    public async Task<JsonWebToken> GenerateAccessTokenAsync(Guid userId, string userEmail, ICollection<string> roles, ICollection<Claim> claims)
    {
        var now = _dateService.CurrentDate();
        var issuer = _authConfig.JwtIssuer;

        var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString())
        };
        
        if (roles?.Any() is true)
            foreach (var role in roles)
                jwtClaims.Add(new Claim("role", role));
        
        if (claims?.Any() is true)
        {
            var customClaims = new List<Claim>();
            foreach (var claim in claims)
            {
                customClaims.Add(new Claim(claim.Type, claim.Value));
            }
            jwtClaims.AddRange(customClaims);
        }

        var expires = now.Add(_authConfig.Expires);
        
        var jwt = new JwtSecurityToken(
            issuer,
            issuer,
            jwtClaims,
            now,
            expires,
            _signingCredentials);
        
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new JsonWebToken
        {
            AccessToken = token,
            Expires = new DateTimeOffset(expires).ToUnixTimeSeconds(),
            UserId = userId,
            Email = userEmail,
            Roles = roles,
            Claims = claims?.ToDictionary(x => x.Type, x => x.Value)
        };
    }
    
    public RefreshToken GenerateRefreshToken()
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = _dateService.CurrentDate().AddDays(_authConfig.RefreshTokenTTL)
        };

        return refreshToken;
    }
}