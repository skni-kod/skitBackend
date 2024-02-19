using skit.Core.Companies.Entities;

namespace skit.Core.Companies.Repositories;

public interface ICompanyRepository
{
    Task<bool> AnyAsync(string name, CancellationToken cancellationToken);
    Task<Company?> GetAsync(Guid companyId, CancellationToken cancellationToken);
    Task<Guid> UpdateAsync(Company company, CancellationToken cancellationToken);
    Task<Guid> CreateAsync(Company company, CancellationToken cancellationToken);
    Task DeleteAsync(Company company, CancellationToken cancellationToken);
}