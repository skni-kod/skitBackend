using MediatR;
using skit.Application.Addresses.Queries.GetAddressForUpdate;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Addresses.Queries.GetAddress;

public sealed class GetAddressHandler : IRequestHandler<GetAddressQuery, GetAddressResponse?>
{
    private readonly EFContext _context;

    public GetAddressHandler(EFContext context)
    {
        _context = context;
    }
    
    public async Task<GetAddressResponse?> Handle(GetAddressQuery request, CancellationToken cancellationToken)
    {
        var address = await _context.Addresses.AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new GetAddressResponse(x.AsDto()))
            .SingleOrDefaultAsync(cancellationToken);

        return address;
    }
}