using skit.Application.Offers.Queries.DTO;
using skit.Core.Offers.Entities;
using skit.Core.Offers.Enums;
using skit.Core.Salaries.Enums;

namespace skit.Infrastructure.DAL.Offers.Queries;

internal static class Extensions
{
    public static OfferDto AsDto(this Offer offer)
    {
        return new OfferDto
        {
            Title = offer.Title,
            CompanyName = offer.Company.Name,
            WorkLocation = offer.WorkLocation,
            Cities = offer.Addresses.Select(address => address.City).ToList(),
            SalaryFrom = offer.Salaries.SingleOrDefault(salary => salary.EmploymentType == SalaryEmploymentType.Permanent)?.SalaryFrom,
            SalaryTo = offer.Salaries.SingleOrDefault(salary => salary.EmploymentType == SalaryEmploymentType.Permanent)?.SalaryTo
        };
    }
}