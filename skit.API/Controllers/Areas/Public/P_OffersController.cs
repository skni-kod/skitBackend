using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using skit.Application.Offers.Queries.BrowseOffers;
using skit.Application.Offers.Queries.BrowsePublicOffers;
using skit.Application.Offers.Queries.GetOffer;
using skit.Application.Offers.Queries.GetPublicOffer;

namespace skit.API.Controllers.Areas.Public;

[AllowAnonymous]
[Route($"{Endpoints.BasePublicUrl}/offers")]
public sealed class P_OffersController : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BrowsePublicOffersResponse>> BrowsePublicOffers([FromQuery] BrowsePublicOffersQuery query, CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(query, cancellationToken);

        return Ok(response);
    }
    
    [HttpGet("{offerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetPublicOfferResponse>> GetPublicOffer([FromRoute] Guid offerId, CancellationToken cancellationToken = default)
    {
        var response = await Mediator.Send(new GetPublicOfferQuery(offerId), cancellationToken);

        return OkOrNotFound(response);
    }

}