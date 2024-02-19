using System.Security.Claims;
using skit.Core.Identity.DTO;

namespace skit.Core.Identity.Services;

public interface ITokenService
{
    Task<JsonWebToken> GenerateAccessTokenAsync(Guid userId, string userEmail, ICollection<string> roles, ICollection<Claim> claims);
    RefreshToken GenerateRefreshToken();
}