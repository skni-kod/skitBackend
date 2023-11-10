using skit.Core.Offers.Entities;

namespace skit.Core.Offers.Repositories;

public interface IOfferRepository
{
    public Task AddAsync(Offer offer, CancellationToken cancellationToken);
}