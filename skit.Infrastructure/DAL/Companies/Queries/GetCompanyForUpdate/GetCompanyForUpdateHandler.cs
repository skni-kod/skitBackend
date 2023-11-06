using MediatR;
using skit.Application.Companies.Queries.GetCompanyForUpdate;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Companies.Queries.GetCompanyForUpdate;

internal sealed class GetCompanyForUpdateHandler : IRequestHandler<GetCompanyForUpdateQuery, GetCompanyForUpdateResponse?>
{
    private readonly EFContext _context;

    public GetCompanyForUpdateHandler(EFContext context)
    {
        _context = context;
    }
    
    public async Task<GetCompanyForUpdateResponse?> Handle(GetCompanyForUpdateQuery query, CancellationToken cancellationToken)
    {
        var company = await _context.Companies.AsNoTracking()
            .Where(c => c.Id == query.CompanyId)
            .Select(c => new GetCompanyForUpdateResponse(c.AsGetCompaniesForUpdateDto()))
            .SingleOrDefaultAsync(cancellationToken);

        return company;
    }
}