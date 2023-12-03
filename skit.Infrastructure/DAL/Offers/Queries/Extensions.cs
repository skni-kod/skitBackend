using skit.Application.Offers.Queries.DTO;
using skit.Core.Common.Extensions;
using skit.Core.Offers.Entities;
using skit.Core.Offers.Enums;
using skit.Core.Salaries.Entities;
using skit.Core.Salaries.Enums;
using skit.Core.Technologies.Entities;

namespace skit.Infrastructure.DAL.Offers.Queries;

internal static class Extensions
{
    public static OfferDto AsDto(this Offer offer)
    {
        var salaries = new List<SalaryDto>();

        foreach (var salary in offer.Salaries)
        {
            salaries.Add(salary.AsDto());
        }

        var technologies = new List<TechnologyDto>();

        foreach (var technology in offer.Technologies)
        {
            technologies.Add(technology.AsDto());
        }
        
        return new OfferDto
        {
            Title = offer.Title,
            CompanyName = offer.Company.Name,
            WorkLocation = offer.WorkLocation.GetValuesFromFlag(),
            Cities = offer.Addresses.Select(address => address.City).ToList(),
            Salaries = salaries,
            Technologies = technologies
        };
    }
    
    public static SalaryDto AsDto(this Salary salary)
    {
        return new SalaryDto
        {
            SalaryFrom = salary.SalaryFrom,
            SalaryTo = salary.SalaryTo,
            EmploymentType = salary.EmploymentType
        };
    }
    
    public static TechnologyDto AsDto(this Technology technology)
    {
        return new TechnologyDto
        {
            Name = technology.Name,
            ThumUrl = technology.ThumUrl
        };
    }
}