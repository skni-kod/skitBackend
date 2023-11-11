using skit.Core.Salaries.Entities;

namespace skit.Core.Salaries.Repositories;

public interface ISalaryRepository
{
    Task<List<Salary>> GetListAsyncForOffer(Guid offerId, IEnumerable<Guid> salaryIds, CancellationToken cancellationToken);
    Task AddAsync(Salary salary, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<Salary> salaries, CancellationToken cancellationToken);
    Task UpdateRangeAsync(IEnumerable<Salary> salaries, CancellationToken cancellationToken);
}