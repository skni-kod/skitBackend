using Microsoft.EntityFrameworkCore;
using skit.Core.Salaries.Entities;
using skit.Core.Salaries.Repositories;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Salaries.Repositories;

internal sealed class SalaryRepository : ISalaryRepository
{
    private readonly EFContext _context;
    private readonly DbSet<Salary> _salaries;

    public SalaryRepository(EFContext context)
    {
        _context = context;
        _salaries = _context.Salaries;
    }

    public Task<List<Salary>> GetListAsyncForOffer(Guid offerId, IEnumerable<Guid> salaryIds, CancellationToken cancellationToken)
        => _salaries.Where(salary => salaryIds.Contains(salary.Id) && salary.OfferId == offerId).ToListAsync(cancellationToken);

        public async Task AddAsync(Salary salary, CancellationToken cancellationToken)
    {
        await _salaries.AddAsync(salary, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<Salary> salaries, CancellationToken cancellationToken)
    {
        _salaries.AddRange(salaries);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRangeAsync(IEnumerable<Salary> salaries, CancellationToken cancellationToken)
    {
        _salaries.UpdateRange(salaries);
        await _context.SaveChangesAsync(cancellationToken);
    }
}