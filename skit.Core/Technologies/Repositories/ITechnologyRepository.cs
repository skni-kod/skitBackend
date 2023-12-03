using skit.Core.Technologies.Entities;

namespace skit.Core.Technologies.Repositories;

public interface ITechnologyRepository
{
    Task<List<Technology>> GetFromIdsListAsync(List<Guid> ids, CancellationToken cancellationToken);
}