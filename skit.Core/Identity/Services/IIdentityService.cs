using Microsoft.AspNetCore.Authentication;
using skit.Core.Identity.DTO;
using skit.Core.Identity.Entities;

namespace skit.Core.Identity.Services;

public interface IIdentityService
{
    Task<Guid> SignUp(string email, string password, CancellationToken cancellationToken);
    Task<JsonWebToken> SignIn(string email, string password, CancellationToken cancellationToken);
    Task SignOut(string? refreshToken, CancellationToken cancellationToken);
    Task<JsonWebToken> RefreshToken(string? refreshToken, CancellationToken cancellationToken);
    Task<ResetPasswordTokenDto> GeneratePasswordResetTokenAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<string> GenerateEmailConfirmationTokenAsync(User user, CancellationToken cancellationToken);
    Task ConfirmAccountAsync(Guid userId, string token, CancellationToken cancellationToken);
    Task ResetPasswordAsync(Guid userId, string token, string password, CancellationToken cancellationToken);
    AuthenticationProperties? ConfigureGoogleAuthentication(string redirectUrl);
    Task<JsonWebToken> GoogleAuthAsync(CancellationToken cancellationToken);
    Task<JsonWebToken> AddCompanyToUserAsync(Guid userId, Guid companyId, CancellationToken cancellationToken);

}