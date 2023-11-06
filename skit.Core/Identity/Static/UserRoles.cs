using Microsoft.AspNetCore.Identity;

namespace skit.Core.Identity.Static;

public static class UserRoles
{
    public const string CompanyOwner = nameof(CompanyOwner);

    private static List<IdentityRole<Guid>> Roles;

    static UserRoles()
    {
        Roles = new List<IdentityRole<Guid>>
        {
            new IdentityRole<Guid>
            {
                Name = CompanyOwner
            }
        };
    }
    
    public static List<IdentityRole<Guid>> Get()
    {
        return Roles;
    }
}