using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using skit.Application.Technologies.Queries.BrowseTechnologies;

namespace skit.API.Controllers.Areas.Public;

[AllowAnonymous]
[Route($"{Endpoints.BasePublicUrl}/technologies")]
public sealed class P_TechnologiesController : BaseController
{
    /// <summary>
    /// Get technologies list
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BrowseTechnologiesResponse>> BrowsePublicOffers([FromQuery] BrowseTechnologiesQuery query, CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(query, cancellationToken);
        
        return Ok(response);
    }
}