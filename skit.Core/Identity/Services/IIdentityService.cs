using skit.Core.Identity.DTO;

namespace skit.Core.Identity.Services;

public interface IIdentityService
{
    Task SignUpCompany(string email, string companyName, string password, CancellationToken cancellationToken);
    Task<JwtWebToken> SignIn(string email, string password, CancellationToken cancellationToken);
}