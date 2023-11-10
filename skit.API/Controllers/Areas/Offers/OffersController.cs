using MediatR;
using Microsoft.AspNetCore.Mvc;
using skit.Application.Offers.Commands.CreateOffer;
using skit.Application.Offers.Commands.UpdateOffer;
using skit.Application.Offers.Queries.BrowseOffers;
using skit.Application.Offers.Queries.BrowseOffers.DTO;

namespace skit.API.Controllers.Areas.Offers;

[Route($"{Endpoints.BaseUrl}/offers")]
public class OffersController : BaseController
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
    public async Task<ActionResult<CreateOfferResponse>> CreateOffer([FromBody] CreateOfferCommand command,
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