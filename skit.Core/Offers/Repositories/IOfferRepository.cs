using skit.Core.Offers.Entities;
using skit.Core.Salaries.Entities;

namespace skit.Core.Offers.Repositories;

public interface IOfferRepository
{
    public Task<List<Offer>> GetAllAsync(CancellationToken cancellationToken);
    public Task<Offer?> GetAsync(Guid offerId, CancellationToken cancellationToken);
    public Task<Guid> AddAsync(Offer offer, CancellationToken cancellationToken);
    public Task<Guid> UpdateAsync(Offer offer, CancellationToken cancellationToken);
    public Task DeleteAsync(Offer offer, CancellationToken cancellationToken);
}