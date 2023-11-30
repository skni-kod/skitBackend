using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Identity;
using skit.Core.Companies.Entities;
using skit.Core.Identity.DTO;
using skit.Core.Identity.Exceptions;

namespace skit.Core.Identity.Entities;

public class User : IdentityUser<Guid>
{
    public Guid? CompanyId { get; set; }
    public Company Company { get; set; }
    
    public bool IsDeleted { get; set; }

    public IReadOnlyCollection<UserRefreshToken> RefreshTokens => _refreshTokens;

    private List<UserRefreshToken> _refreshTokens = new();

    public void AddRefreshToken(RefreshToken refreshToken)
    {
        var token = UserRefreshToken.Create(refreshToken.Token, refreshToken.Expires);
        _refreshTokens.Add(token);
    }

    public void DeleteRefreshToken(UserRefreshToken refreshToken)
    {
        _refreshTokens.Remove(refreshToken);
    }

}