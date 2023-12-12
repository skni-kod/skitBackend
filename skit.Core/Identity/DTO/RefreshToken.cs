namespace skit.Core.Identity.DTO;

public sealed class RefreshToken
{
    public string Token { get; set; }
    public DateTimeOffset Expires { get; set; }
}