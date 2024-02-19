using EFCoreSecondLevelCacheInterceptor;
using MediatR;
using skit.Application.Addresses.Queries.BrowseAddresses;
using skit.Application.Technologies.Queries.BrowseTechnologies;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Technologies.Queries.BrowseTechnologies;

public sealed class BrowseTechnologiesHandler : IRequestHandler<BrowseTechnologiesQuery, BrowseTechnologiesResponse>
{
    private readonly EFContext _context;

    public BrowseTechnologiesHandler(EFContext context)
    {
        _context = context;
    }
    
    public async Task<BrowseTechnologiesResponse> Handle(BrowseTechnologiesQuery request, CancellationToken cancellationToken)
    {
        var query = await _context.Technologies
            .AsNoTracking()
            .Cacheable(CacheExpirationMode.Absolute, TimeSpan.FromHours(24))
            .ToListAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(request.Search))
            query = query
                .Where(x => EFCore.Functions.ILike(x.Name, $"%{request.Search}%")).ToList();

        var technologies = query
            .Select(x => x.AsDto())
            .ToList();

        return new BrowseTechnologiesResponse(technologies);
    }
}