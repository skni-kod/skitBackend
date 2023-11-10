﻿using Microsoft.EntityFrameworkCore;
using skit.Core.Offers.Entities;
using skit.Core.Offers.Repositories;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Offers.Repositories;

internal sealed class OfferRepository : IOfferRepository
{
    private readonly EFContext _context;
    private readonly DbSet<Offer> _offers;

    public OfferRepository(EFContext context)
    {
        _context = context;
        _offers = _context.Offers;
    }

    public async Task AddAsync(Offer offer, CancellationToken cancellationToken)
    {
        await _offers.AddAsync(offer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}