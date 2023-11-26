using Microsoft.AspNetCore.Mvc;
using skit.API.Attributes;
using skit.Application.Addresses.Commands.CreateAddress;
using skit.Application.Addresses.Commands.DeleteAddress;
using skit.Application.Addresses.Commands.UpdateAddress;
using skit.Application.Addresses.Queries.BrowseAddresses;
using skit.Application.Addresses.Queries.GetAddress;
using skit.Core.Identity.Static;
using skit.Shared.Responses;

namespace skit.API.Controllers.Areas.CompanyOwner;

[Route($"{Endpoints.BaseUrl}/addresses")]
[ApiAuthorize(Roles = UserRoles.CompanyOwner)]
public sealed class C_AddressesController : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BrowseAddressesResponse>> BrowseAddresses([FromQuery] BrowseAddressesQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("{id::guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetAddressResponse>> BrowseAddresses([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAddressQuery(id), cancellationToken);
        return OkOrNotFound(result);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateOrUpdateResponse>> CreateAddress([FromBody] CreateAddressCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Created(string.Empty, result);
    }
    
    [HttpPut("{id::guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateOrUpdateResponse>> UpdateAddress([FromRoute] Guid id, [FromBody] UpdateAddressCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete("{id::guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAddress([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteAddressCommand(id), cancellationToken);
        return Ok();
    }
    
}