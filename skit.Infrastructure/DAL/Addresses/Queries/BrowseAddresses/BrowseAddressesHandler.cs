using MediatR;
using skit.Application.Addresses.Queries.BrowseAddresses;
using skit.Core.Common.Services;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Addresses.Queries.BrowseAddresses;

public sealed class BrowseAddressesHandler : IRequestHandler<BrowseAddressesQuery, BrowseAddressesResponse>
{
    private readonly EFContext _context;
    private readonly ICurrentUserService _currentUserService;

    public BrowseAddressesHandler(EFContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    
    public async Task<BrowseAddressesResponse> Handle(BrowseAddressesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Addresses.AsNoTracking()
            .Where(x => x.CompanyId == _currentUserService.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.Search))
            query = query
                .Where(x => EFCore.Functions.ILike(x.City, $"%{request.Search}%") ||
                            EFCore.Functions.ILike(x.Street, $"%{request.Search}%") ||
                            EFCore.Functions.ILike(x.HouseNumber, $"%{request.Search}%") ||
                            EFCore.Functions.ILike(x.PostalCode, $"%{request.Search}%"));

        var addresses = await query
            .Select(x => x.AsDto())
            .ToListAsync(cancellationToken);

        return new BrowseAddressesResponse(addresses);
    }
}