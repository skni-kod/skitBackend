using Microsoft.EntityFrameworkCore;
using skit.Core.Companies.Entities;
using skit.Core.Companies.Repositories;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Companies.Repositories;

internal sealed class CompanyRepository : ICompanyRepository
{
    private readonly EFContext _context;
    private readonly DbSet<Company> _companies;

    public CompanyRepository(EFContext context)
    {
        _context = context;
        _companies = _context.Companies;
    }

    public async Task<bool> AnyAsync(string name, CancellationToken cancellationToken)
    {
        return await _companies.AnyAsync(x => EFCore.Functions.ILike(x.Name, name), cancellationToken);
    }

    public async Task<Company?> GetAsync(Guid companyId, CancellationToken cancellationToken)
        => await _companies.SingleOrDefaultAsync(company => company.Id == companyId, cancellationToken: cancellationToken);


    public async Task<Guid> UpdateAsync(Company company, CancellationToken cancellationToken)
    {
        var result = _companies.Update(company);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity.Id;
    }

    public async Task<Guid> CreateAsync(Company company, CancellationToken cancellationToken)
    {
        var result = await _companies.AddAsync(company, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity.Id;
    }

    public async Task DeleteAsync(Company company, CancellationToken cancellationToken)
    {
        _companies.Remove(company);
        await _context.SaveChangesAsync(cancellationToken);
    }
}