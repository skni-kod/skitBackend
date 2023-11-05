using skit.Application.Companies.Queries.DTO;
using skit.Application.Companies.Queries.GetCompaniesForUpdate.DTO;
using skit.Core.Companies.Entities;
using skit.Core.Companies.Enums;

namespace skit.Infrastructure.DAL.Companies.Queries;

internal static class Extensions
{
    public static CompanyDto AsDto(this Company company)
    {
        return new CompanyDto
        {
            Name = company.Name,
            Size = company.Size,
        };
    }
    
    public static GetCompaniesForUpdateDto AsGetCompaniesForUpdateDto(this Company company)
    {
        return new GetCompaniesForUpdateDto
        {
            Name = company.Name,
            Description = company.Description,
            Size = company.Size,
            Links = company.Links,
        };
    }
}