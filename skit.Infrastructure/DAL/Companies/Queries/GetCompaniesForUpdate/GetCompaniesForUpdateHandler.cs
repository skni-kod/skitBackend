using MediatR;
using Microsoft.EntityFrameworkCore;
using skit.Application.Companies.Queries.GetCompaniesForUpdate;
using skit.Application.Companies.Queries.GetCompaniesForUpdate.DTO;
using skit.Core.Companies.Exceptions;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Companies.Queries.GetCompaniesForUpdate;

internal sealed class GetCompaniesForUpdateHandler : IRequestHandler<GetCompaniesForUpdateQuery, GetCompaniesForUpdateDto>
{
    private readonly EFContext _context;

    public GetCompaniesForUpdateHandler(EFContext context)
    {
        _context = context;
    }
    
    public async Task<GetCompaniesForUpdateDto> Handle(GetCompaniesForUpdateQuery query, CancellationToken cancellationToken)
    {
        var company = await _context.Companies.AsNoTracking()
            .SingleOrDefaultAsync(company => company.Id == query.CompanyId, cancellationToken)
            ?? throw new CompanyNotFoundException();

        return company.AsGetCompaniesForUpdateDto();
    }
}