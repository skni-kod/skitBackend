using skit.Core.Addresses.Entities;
using skit.Core.Technologies.Entities;
using skit.Core.Technologies.Repositories;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Technologies.Repositories;

internal sealed class TechnologyRepository : ITechnologyRepository
{
    private readonly EFContext _context;
    private readonly DbSet<Technology> _technologies;

    public TechnologyRepository(EFContext context)
    {
        _context = context;
        _technologies = _context.Technologies;
    }

    public async Task<List<Technology>> GetFromIdsListAsync(List<Guid> ids, CancellationToken cancellationToken)
        => await _technologies.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
}