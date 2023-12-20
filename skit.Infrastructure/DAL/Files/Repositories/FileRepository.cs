using skit.Core.Files.Entities;
using skit.Core.Files.Repositories;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Files.Repositories;

public sealed class FileRepository : IFileRepository
{
    private readonly EFContext _context;
    private readonly DbSet<SystemFile> _files;

    public FileRepository(EFContext context)
    {
        _context = context;
        _files = context.Files;
    }
    
    public async Task<Guid> AddAsync(SystemFile file, CancellationToken cancellationToken)
    {
        var result = await _context.AddAsync(file, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity.Id;
    }

    public async Task<SystemFile?> GetAsync(Guid id, CancellationToken cancellationToken)
        => await _files.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<Guid> UpdateAsync(SystemFile file, CancellationToken cancellationToken)
    {
        var result = _context.Update(file);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity.Id;
    }

    public async Task DeleteAsync(SystemFile file, CancellationToken cancellationToken)
    {
        _context.Remove(file);
        await _context.SaveChangesAsync(cancellationToken);
    }
}