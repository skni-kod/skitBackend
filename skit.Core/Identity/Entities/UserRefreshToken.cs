using Microsoft.EntityFrameworkCore;
using skit.Core.Identity.Exceptions;

namespace skit.Core.Identity.Entities;

[Owned]
public sealed class UserRefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public DateTimeOffset Expires { get; set; }
    public bool IsExpired => DateTimeOffset.UtcNow >= Expires;
    
    private UserRefreshToken() {}

    private UserRefreshToken(string token, DateTimeOffset expires)
    {
        Token = token;
        Expires = expires;
    }

    public static UserRefreshToken Create(string token, DateTimeOffset expires)
        => new(token, expires);

    public void Validation()
    {
        if (IsExpired)
            throw new InvalidRefreshTokenException();
    }
}