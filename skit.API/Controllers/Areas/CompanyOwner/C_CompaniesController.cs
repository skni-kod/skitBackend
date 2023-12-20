using Microsoft.AspNetCore.Mvc;
using skit.API.Attributes;
using skit.Application.Companies.Commands.DeleteCompany;
using skit.Application.Companies.Commands.UpdateCompany;
using skit.Application.Companies.Queries.CompanyOwner.GetCompanyForUpdate;
using skit.Core.Identity.Static;

namespace skit.API.Controllers.Areas.CompanyOwner;

[Route($"{Endpoints.BaseUrl}/companies")]
[ApiAuthorize(Roles = UserRoles.CompanyOwner)]
public class C_CompaniesController : BaseController
{
    /// <summary>
    /// Update company by Id
    /// </summary>
    [HttpPut("{companyId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> UpdateCompanies([FromRoute] Guid companyId, [FromBody] UpdateCompanyCommand command, CancellationToken cancellationToken = default)
    {
        command.CompanyId = companyId;
        await Mediator.Send(command, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Get company by Id for update
    /// </summary>
    [HttpGet("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetCompanyForUpdateResponse>> GetCompaniesForUpdate(CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(new GetCompanyForUpdateQuery(), cancellationToken);
        return OkOrNotFound(response);
    }
    
    /// <summary>
    /// Delete company by Id
    /// </summary>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteCompany(CancellationToken cancellationToken = default)
    {
        await Mediator.Send(new DeleteCompanyCommand(), cancellationToken);
        return Ok();
    }
}