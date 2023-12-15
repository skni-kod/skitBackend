using Microsoft.AspNetCore.Mvc;
using skit.API.Attributes;
using skit.Application.Offers.Commands.CreateOffer;
using skit.Application.Offers.Commands.DeleteOffer;
using skit.Application.Offers.Commands.UpdateOffer;
using skit.Application.Offers.Queries.BrowseOffers;
using skit.Application.Offers.Queries.GetOffer;
using skit.Application.Offers.Queries.GetOfferForUpdate;
using skit.Core.Identity.Static;
using skit.Shared.Responses;

namespace skit.API.Controllers.Areas.CompanyOwner;

[Route($"{Endpoints.BaseUrl}/offers")]
[ApiAuthorize(Roles = UserRoles.CompanyOwner)]
public class C_OffersController : BaseController
{
    /// <summary>
    /// Get company offers paginated list
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BrowseOffersResponse>> BrowseOffers([FromQuery] BrowseOffersQuery query, CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(query, cancellationToken);

        return Ok(response);
    }
    
    /// <summary>
    /// Get company offer by Id
    /// </summary>
    [HttpGet("{offerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetOfferResponse>> GetOffer([FromRoute] Guid offerId, CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(new GetOfferQuery(offerId), cancellationToken);

        return OkOrNotFound(response);
    }
    
    /// <summary>
    /// Get company offer by Id for update
    /// </summary>
    [HttpGet("{offerId:guid}/update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetOfferForUpdateResponse>> GetOfferForUpdate([FromRoute] Guid offerId, CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(new GetOfferForUpdateQuery(offerId), cancellationToken);

        return OkOrNotFound(response);
    }

    /// <summary>
    /// Create offer
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateOrUpdateResponse>> CreateOffer([FromBody] CreateOfferCommand command,
        CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(command, cancellationToken);

        return Ok(response);
    }
    
    /// <summary>
    /// Update offer by Id
    /// </summary>
    [HttpPut("{offerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateOrUpdateResponse>> UpdateOffer([FromRoute] Guid offerId, [FromBody] UpdateOfferCommand command,
        CancellationToken cancellationToken = default)
    {
        command.OfferId = offerId;
        var response = await Mediator.Send(command, cancellationToken);

        return Ok(response);
    }
    
    /// <summary>
    /// Delete offer by Id
    /// </summary>
    [HttpDelete("{offerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteOffer([FromRoute] Guid offerId, CancellationToken cancellationToken = default)
    {
        await Mediator.Send(new DeleteOfferCommand(offerId), cancellationToken);

        return Ok();
    }
}