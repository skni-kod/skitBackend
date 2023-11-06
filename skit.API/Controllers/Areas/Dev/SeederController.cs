using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using skit.Core.Identity.Static;

namespace skit.API.Controllers.Areas.Dev;

[Route("seed")]
public sealed class SeederController : BaseController
{
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public SeederController(RoleManager<IdentityRole<Guid>> roleManager)
    {
        _roleManager = roleManager;
    }
    
    [HttpPost("roles")]
    public async Task<IActionResult> SeedRoles(CancellationToken cancellationToken)
    {
        var roles = UserRoles.Get();
        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role.Name))
            {
                await _roleManager.CreateAsync(role);
            }
        }

        return Ok();
    }
    
}