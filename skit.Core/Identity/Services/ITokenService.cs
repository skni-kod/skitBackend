using System.Security.Claims;
using skit.Core.Identity.DTO;

namespace skit.Core.Identity.Services;

public interface ITokenService
{
    Task<JsonWebToken> GenerateAccessToken(Guid userId, string userEmail, ICollection<string> roles, ICollection<Claim> claims);
    Task<JsonWebToken> GenerateGoogleAccessToken(ClaimsPrincipal principal, Guid userId, string userEmail, ICollection<string> roles);
    RefreshToken GenerateRefreshToken();
}