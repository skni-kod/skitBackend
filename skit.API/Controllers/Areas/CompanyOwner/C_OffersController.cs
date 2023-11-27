using Microsoft.AspNetCore.Mvc;
using skit.API.Attributes;
using skit.Application.Offers.Commands.CreateOffer;
using skit.Application.Offers.Commands.UpdateOffer;
using skit.Application.Offers.Queries.BrowseOffers;
using skit.Application.Offers.Queries.BrowseOffers.DTO;
using skit.Core.Identity.Static;
using skit.Shared.Responses;

namespace skit.API.Controllers.Areas.CompanyOwner;

[Route($"{Endpoints.BaseUrl}/offers")]
[ApiAuthorize(Roles = UserRoles.CompanyOwner)]
public class C_OffersController : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BrowseOffersDto>> BrowseOffers([FromQuery] BrowseOffersQuery query, CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CreateOrUpdateResponse>> CreateOffer([FromBody] CreateOfferCommand command,
        CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(command, cancellationToken);

        return Ok(response);
    }
    
    [HttpPut("{offerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> UpdateOffer([FromRoute] Guid offerId, [FromBody] UpdateOfferCommand command,
        CancellationToken cancellationToken = default)
    {
        command.OfferId = offerId;
        await Mediator.Send(command, cancellationToken);

        return Ok();
    }
}