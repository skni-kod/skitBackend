using Microsoft.AspNetCore.Identity;
using skit.Core.Companies.Entities;

namespace skit.Core.Identity.Entities;

public class User : IdentityUser<Guid>
{
    public Guid? CompanyId { get; set; }
    public Company Company { get; set; }
    
    public bool IsDeleted { get; set; }
}