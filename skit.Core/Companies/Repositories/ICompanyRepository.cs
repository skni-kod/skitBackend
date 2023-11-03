using skit.Core.Companies.Entities;

namespace skit.Core.Companies.Repositories;

public interface ICompanyRepository
{
    Task<Company> GetAsync(Guid companyId, CancellationToken cancellationToken);
    Task UpdateAsync(Company company, CancellationToken cancellationToken);
    Task DeleteAsync(Company company, CancellationToken cancellationToken);
}