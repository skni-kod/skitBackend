using Microsoft.AspNetCore.Mvc;
using skit.Application.JobApplications.Commands.UpdateJobApplication;
using skit.Application.JobApplications.Queries.GetJobApplication;
using skit.Shared.Responses;

namespace skit.API.Controllers.Areas.CompanyOwner;

[Route($"{Endpoints.BaseUrl}/jobApplications")]
public sealed class P_JobApplicationsController : BaseController
{
    /// <summary>
    /// Get job application by id
    /// </summary>
    [HttpGet("{jobApplicationId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetJobApplicationResponse>> GetJobApplication([FromRoute] Guid jobApplicationId, 
        CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(new GetJobApplicationQuery(jobApplicationId), cancellationToken);
        return OkOrNotFound(response);
    }
    
    /// <summary>
    /// Update application
    /// </summary>
    [HttpPut("{jobApplicationId::guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateOrUpdateResponse>> UpdateJobApplication([FromRoute] Guid jobApplicationId, 
        [FromBody] UpdateJobApplicationCommand command, CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(command with {JobApplicationId = jobApplicationId}, cancellationToken);
        return Ok(response);
    }
}