﻿using Microsoft.AspNetCore.Mvc;
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
}