namespace skit.Core.Identity.DTO;

public sealed class JwtWebToken
{
    public string AccessToken { get; init; }
    public long Expires { get; init; }
    public Guid UserId { get; init; }
    public string Email { get; set; }
    public ICollection<string>? Roles { get; init; }
    public IDictionary<string, string>? Claims { get; init; }
}