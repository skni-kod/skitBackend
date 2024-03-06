using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using skit.Application.Files.Queries.GetFile;

namespace skit.API.Controllers.Areas.Public;

[AllowAnonymous]
[Route($"{Endpoints.BasePublicUrl}/files")]
public class P_FilesController : BaseController
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetFile(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetFileQuery(id), cancellationToken);
        return File(result.Content, result.ContentType, result.FileName);
    }
}