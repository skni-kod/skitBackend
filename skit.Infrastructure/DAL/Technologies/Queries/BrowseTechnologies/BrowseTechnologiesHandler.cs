using EFCoreSecondLevelCacheInterceptor;
using MediatR;
using skit.Application.Addresses.Queries.BrowseAddresses;
using skit.Application.Technologies.Queries.BrowseTechnologies;
using skit.Core.Files.Services;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Technologies.Queries.BrowseTechnologies;

public sealed class BrowseTechnologiesHandler : IRequestHandler<BrowseTechnologiesQuery, BrowseTechnologiesResponse>
{
    private readonly EFContext _context;
    private readonly IS3StorageService _s3StorageService;

    public BrowseTechnologiesHandler(EFContext context, IS3StorageService s3StorageService)
    {
        _context = context;
        _s3StorageService = s3StorageService;
    }
    
    public async Task<BrowseTechnologiesResponse> Handle(BrowseTechnologiesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Technologies.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Search))
            query = query
                .Where(x => EFCore.Functions.ILike(x.Name, $"%{request.Search}%"));

        var technologies = query
            .Include(x => x.Photo)
            .Select(x => x.AsDto(x.PhotoId == null ? null : _s3StorageService.GetFileUrl(x.Photo.S3Key, x.Photo.Name)))
            .ToList();

        return new BrowseTechnologiesResponse(technologies);
    }
}