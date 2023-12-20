using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using skit.Core.Common.Services;
using skit.Core.Identity.Entities;
using skit.Core.Identity.Exceptions;
using skit.Core.Identity.Static;

namespace skit.Infrastructure.Common.Services;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<User> _userManager;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public Guid UserId => GetClaimAsGuid(ClaimTypes.NameIdentifier, _httpContextAccessor);
    public Guid CompanyId => GetClaimAsGuid(UserClaims.CompanyId, _httpContextAccessor);
    public async Task<bool> IsEmailConfirmedAsync(CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == UserId, cancellationToken)
                   ?? throw new UserNotFoundException();

        return user.EmailConfirmed;
    }

    private static Guid GetClaimAsGuid(string claimType, IHttpContextAccessor httpContextAccessor)
    {
        var claimAsString = httpContextAccessor?.HttpContext?.User.FindFirstValue(claimType);

        if (string.IsNullOrWhiteSpace(claimAsString))
            return Guid.Empty;

        var parseResultSuccessful = Guid.TryParse(claimAsString, out var claimId);

        if (!parseResultSuccessful || claimId == Guid.Empty)
            return Guid.Empty;

        return claimId;
    }
}