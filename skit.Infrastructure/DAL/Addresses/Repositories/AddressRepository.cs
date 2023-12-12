using skit.Core.Addresses.Entities;
using skit.Core.Addresses.Repositories;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Addresses.Repositories;

public sealed class AddressRepository : IAddressRepository
{
    private readonly EFContext _context;
    private readonly DbSet<Address> _addresses;

    public AddressRepository(EFContext context)
    {
        _context = context;
        _addresses = context.Addresses;
    }
    
    public async Task<Guid> AddAsync(Address address, CancellationToken cancellationToken)
    {
        var result = await _addresses.AddAsync(address, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity.Id;
    }

    public async Task<Address?> GetAsync(Guid id, CancellationToken cancellationToken)
        => await _addresses.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<List<Address>> GetFromIdsListForCompanyAsync(List<Guid> ids, Guid companyId, CancellationToken cancellationToken)
        => await _addresses.Where(x => ids.Contains(x.Id) && x.CompanyId == companyId).ToListAsync(cancellationToken);

    public async Task<Guid> UpdateAsync(Address address, CancellationToken cancellationToken)
    {
        var result = _addresses.Update(address);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity.Id;
    }

    public async Task DeleteAsync(Address address, CancellationToken cancellationToken)
    {
        _addresses.Remove(address);
        await _context.SaveChangesAsync(cancellationToken);
    }
}