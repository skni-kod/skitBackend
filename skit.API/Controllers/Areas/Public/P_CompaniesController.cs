using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using skit.Application.Companies.Queries.Public.BrowseCompanies;

namespace skit.API.Controllers.Areas.Public;

[AllowAnonymous]
[Route($"{Endpoints.BasePublicUrl}/companies")]
public sealed class P_CompaniesController : BaseController
{
    /// <summary>
    /// Get public companies paginated list
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BrowseCompaniesResponse>> BrowseCompanies([FromQuery] BrowseCompaniesQuery query, CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(query, cancellationToken);
        return Ok(response);
    }
}