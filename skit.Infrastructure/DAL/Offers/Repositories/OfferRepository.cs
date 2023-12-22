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

    public async Task<List<Offer>> GetAllAsync(CancellationToken cancellationToken)
        => await _offers.ToListAsync(cancellationToken);

    public async Task<Offer?> GetAsync(Guid offerId, CancellationToken cancellationToken)
        => await _offers
            .Include(offer => offer.Salaries)
            .Include(offer => offer.Addresses)
            .Include(offer => offer.Technologies)
            .SingleOrDefaultAsync(offer => offer.Id == offerId, cancellationToken);

    public async Task<Guid> AddAsync(Offer offer, CancellationToken cancellationToken)
    {
        var result = await _offers.AddAsync(offer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }    
    
    public async Task<Guid> UpdateAsync(Offer offer, CancellationToken cancellationToken)
    {
        var result = _offers.Update(offer);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }

    public async Task DeleteAsync(Offer offer, CancellationToken cancellationToken)
    {
        _offers.Remove(offer);
        await _context.SaveChangesAsync(cancellationToken);
    }
}