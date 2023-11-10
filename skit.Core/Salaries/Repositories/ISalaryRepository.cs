using skit.Core.Salaries.Entities;

namespace skit.Core.Salaries.Repositories;

public interface ISalaryRepository
{
    Task AddAsync(Salary salary, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<Salary> salaries, CancellationToken cancellationToken);
}