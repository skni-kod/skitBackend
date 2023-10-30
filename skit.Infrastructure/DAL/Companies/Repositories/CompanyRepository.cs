﻿using Microsoft.EntityFrameworkCore;
using skit.Core.Companies.Entities;
using skit.Core.Companies.Repositories;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Companies.Repositories;

internal sealed class CompanyRepository : ICompanyRepository
{
    private readonly EFContext _context;

    public CompanyRepository(EFContext context)
    {
        _context = context;
    }

    public async Task<Company> GetAsync(Guid companyId, CancellationToken cancellationToken)
        => await _context.Companies.SingleOrDefaultAsync(company => company.Id == companyId, cancellationToken: cancellationToken);


    public async Task UpdateAsync(Company company, CancellationToken cancellationToken)
    {
        _context.Companies.Update(company);
        await _context.SaveChangesAsync(cancellationToken);
    }
}