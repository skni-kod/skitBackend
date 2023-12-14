using skit.Core.Files.Entities;

namespace skit.Core.Files.Repositories;

public interface IFileRepository
{
    Task<Guid> AddAsync(SystemFile file, CancellationToken cancellationToken);
    Task<SystemFile?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<Guid> UpdateAsync(SystemFile file, CancellationToken cancellationToken);
    Task DeleteAsync(SystemFile file, CancellationToken cancellationToken);
}