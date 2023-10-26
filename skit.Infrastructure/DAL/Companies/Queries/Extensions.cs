using skit.Application.Companies.Queries.DTO;
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
}