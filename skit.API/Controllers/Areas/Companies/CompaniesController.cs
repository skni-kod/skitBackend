using Microsoft.AspNetCore.Mvc;
using skit.Application.Companies.Commands.DeleteCompany;
using skit.Application.Companies.Commands.UpdateCompany;
using skit.Application.Companies.Queries.BrowseCompanies;
using skit.Application.Companies.Queries.BrowseCompanies.DTO;

namespace skit.API.Controllers.Areas.Companies;

[Route($"{Endpoints.BaseUrl}/companies")]
public class CompaniesController : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BrowseCompaniesDto>> BrowseCompanies([FromQuery] BrowseCompaniesQuery query, CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(query, cancellationToken);
        return Ok(response);
    }
    
    [HttpPut("{companyId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> UpdateCompanies([FromRoute] Guid companyId, [FromBody] UpdateCompanyCommand command, CancellationToken cancellationToken = default)
    {
        command.CompanyId = companyId;
        await Mediator.Send(command, cancellationToken);
        return Ok();
    }
    
    [HttpDelete("{companyId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteCompany([FromRoute] Guid companyId, CancellationToken cancellationToken = default)
    {
        await Mediator.Send(new DeleteCompanyCommand(companyId), cancellationToken);
        return Ok();
    }
}