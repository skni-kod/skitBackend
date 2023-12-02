﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using skit.Application.Offers.Queries.BrowseOffers;
using skit.Application.Offers.Queries.BrowseOffers.DTO;
using skit.Core.Companies.Entities;
using skit.Infrastructure.DAL.EF.Context;
using skit.Shared.Abstractions;
using skit.Shared.Abstractions.Extensions;

namespace skit.Infrastructure.DAL.Offers.Queries.BrowseOffers;

internal sealed class BrowseOffersHandler : IRequestHandler<BrowseOffersQuery, BrowseOffersDto>
{
    private readonly EFContext _context;

    public BrowseOffersHandler(EFContext context)
    {
        _context = context;
    }

    public async Task<BrowseOffersDto> Handle(BrowseOffersQuery query, CancellationToken cancellationToken)
    {
        var offers = _context.Offers.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var searchTxt = $"%{query.Search}%";

            offers = offers.Where(offer => Microsoft.EntityFrameworkCore.EF.Functions.ILike(offer.Title, searchTxt) || 
                            offer.Description != null && Microsoft.EntityFrameworkCore.EF.Functions.ILike(offer.Description, searchTxt));
        }

        var result = await offers
            .Include(offer => offer.Addresses)
            .Include(offer => offer.Salaries)
            .Include(offer => offer.Company)
            .Select(offer => offer.AsDto())
            .ToPaginatedListAsync(query);

        return new BrowseOffersDto(result);
    }
}