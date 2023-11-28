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

    public async Task<Offer?> GetAsync(Guid offerId, CancellationToken cancellationToken)
        => await _offers
            .Include(offer => offer.Salaries)
            .Include(offer => offer.Addresses)
            .SingleOrDefaultAsync(offer => offer.Id == offerId, cancellationToken);

        public async Task AddAsync(Offer offer, CancellationToken cancellationToken)
    {
        await _offers.AddAsync(offer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }    
    
    public async Task UpdateAsync(Offer offer, CancellationToken cancellationToken)
    {
        _offers.Update(offer);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Offer offer, CancellationToken cancellationToken)
    {
        _offers.Remove(offer);
        await _context.SaveChangesAsync(cancellationToken);
    }
}