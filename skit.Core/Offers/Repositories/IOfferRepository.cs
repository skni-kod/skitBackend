using skit.Core.Offers.Entities;

namespace skit.Core.Offers.Repositories;

public interface IOfferRepository
{
    public Task<Offer> GetAsync(Guid offerId, CancellationToken cancellationToken);
    public Task AddAsync(Offer offer, CancellationToken cancellationToken);
    public Task UpdateAsync(Offer offer, CancellationToken cancellationToken);
    // public Task<bool> SalariesExistInOffer(Guid offerId, IEnumerable<Guid> salaryIds, CancellationToken cancellationToken);
}