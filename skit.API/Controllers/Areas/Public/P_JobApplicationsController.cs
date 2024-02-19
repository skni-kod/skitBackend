using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using skit.Application.JobApplications.Commands.CreateJobApplication;
using skit.Application.JobApplications.Commands.UpdateJobApplication;
using skit.Shared.Responses;

namespace skit.API.Controllers.Areas.Public;

[AllowAnonymous]
[Route($"{Endpoints.BasePublicUrl}/jobApplications")]
public sealed class P_JobApplicationsController : BaseController
{
    /// <summary>
    /// Create application
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateOrUpdateResponse>> CreateJobApplication([FromBody] CreateJobApplicationCommand command, 
        CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(command, cancellationToken);
        return Created(string.Empty, response);
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