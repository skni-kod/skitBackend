using MediatR;
using skit.Application.Companies.Queries.GetCompanyForUpdate;
using skit.Core.Common.Services;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Companies.Queries.GetCompanyForUpdate;

internal sealed class GetCompanyForUpdateHandler : IRequestHandler<GetCompanyForUpdateQuery, GetCompanyForUpdateResponse?>
{
    private readonly EFContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetCompanyForUpdateHandler(EFContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    
    public async Task<GetCompanyForUpdateResponse?> Handle(GetCompanyForUpdateQuery query, CancellationToken cancellationToken)
    {
        var company = await _context.Companies.AsNoTracking()
            .Where(c => c.Id == _currentUserService.CompanyId)
            .Select(c => new GetCompanyForUpdateResponse(c.AsGetCompaniesForUpdateDto()))
            .SingleOrDefaultAsync(cancellationToken);

        return company;
    }
}