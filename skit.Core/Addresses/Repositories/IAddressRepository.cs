using skit.Core.Addresses.Entities;

namespace skit.Core.Addresses.Repositories;

public interface IAddressRepository
{
    Task<Guid> AddAsync(Address address, CancellationToken cancellationToken);
    Task<Address?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Address>> GetFromIdsListForCompanyAsync(List<Guid> ids, Guid companyId, CancellationToken cancellationToken);
    Task<Guid> UpdateAsync(Address address, CancellationToken cancellationToken);
    Task DeleteAsync(Address address, CancellationToken cancellationToken);
}