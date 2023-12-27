using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using skit.Application.JobApplications.Commands.CreateJobApplication;
using skit.Shared.Responses;

namespace skit.API.Controllers.Areas.Public;

[AllowAnonymous]
[Route($"{Endpoints.BasePublicUrl}/offers")]
public sealed class P_JobApplicationsController : BaseController
{
    /// <summary>
    /// Create application
    /// </summary>
    [HttpPost("{offerId:guid}/applications")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateOrUpdateResponse>> CreateJobApplication([FromRoute] Guid offerId, 
        [FromBody] CreateJobApplicationCommand command, CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(command with {OfferId = offerId}, cancellationToken);
        return Created(string.Empty, response);
    }
}