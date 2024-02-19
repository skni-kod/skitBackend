using Microsoft.AspNetCore.Mvc;
using skit.Application.JobApplications.Queries.GetJobApplication;

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
}