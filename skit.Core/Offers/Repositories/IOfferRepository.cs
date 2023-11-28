using skit.Core.Offers.Entities;
using skit.Core.Salaries.Entities;

namespace skit.Core.Offers.Repositories;

public interface IOfferRepository
{
    public Task<Offer?> GetAsync(Guid offerId, CancellationToken cancellationToken);
    public Task AddAsync(Offer offer, CancellationToken cancellationToken);
    public Task UpdateAsync(Offer offer, CancellationToken cancellationToken);
    public Task DeleteAsync(Offer offer, CancellationToken cancellationToken);
}