using Microsoft.AspNetCore.Mvc;
using skit.API.Attributes;
using skit.API.Common;
using skit.Application.Companies.Commands.CreateCompany;
using skit.Application.Companies.Commands.DeleteCompany;
using skit.Application.Companies.Commands.UpdateCompany;
using skit.Application.Companies.Queries.CompanyOwner.GetCompanyForUpdate;
using skit.Core.Identity.DTO;
using skit.Core.Identity.Static;
using skit.Shared.Responses;

namespace skit.API.Controllers.Areas.CompanyOwner;

[Route($"{Endpoints.BaseUrl}/companies")]
public class C_CompaniesController : BaseController
{
    /// <summary>
    /// Create company
    /// </summary>
    [ApiAuthorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<JsonWebToken>> CreateCompany([FromBody] CreateCompanyCommand command, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(command, cancellationToken);
        SetRefreshTokenCookie(result.RefreshToken);
        return Ok(result);
    }
    
    /// <summary>
    /// Update company by Id
    /// </summary>
    [ApiAuthorize(Roles = UserRoles.CompanyOwner)]
    [HttpPut("{companyId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CreateOrUpdateResponse>> UpdateCompany([FromRoute] Guid companyId, [FromBody] UpdateCompanyCommand command, CancellationToken cancellationToken = default)
    {
        command.CompanyId = companyId;
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get company by Id for update
    /// </summary>
    [ApiAuthorize(Roles = UserRoles.CompanyOwner)]
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
    [ApiAuthorize(Roles = UserRoles.CompanyOwner)]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteCompany(CancellationToken cancellationToken = default)
    {
        await Mediator.Send(new DeleteCompanyCommand(), cancellationToken);
        return Ok();
    }
}