using Microsoft.AspNetCore.Mvc;
using skit.Application.Companies.Queries.BrowseCompanies;

namespace skit.API.Controllers.Areas.Companies;

[Route($"{Endpoints.BaseUrl}/companies")]
public class CompaniesController : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BrowseCompaniesQuery>>> BrowseCompanies([FromQuery] BrowseCompaniesQuery query, CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(query, cancellationToken);
        return Ok(response);
    }
}