using skit.Application.Offers.Commands.UpdateOffer;
using skit.Application.Offers.Queries.DTO;
using skit.Application.Offers.Queries.GetOfferForUpdate.DTO;
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
            Id = offer.Id,
            Title = offer.Title,
            CompanyName = offer.Company.Name,
            WorkLocation = offer.WorkLocation.GetValuesFromFlag(),
            Cities = offer.Addresses.Select(address => address.City).ToList(),
            Salaries = salaries,
            Technologies = technologies
        };
    }
    
    public static OfferDetailsDto AsDetailsDto(this Offer offer)
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
        
         return new OfferDetailsDto
         {
             Id = offer.Id,
             Title = offer.Title,
             Description = offer.Description, 
             DateFrom = offer.DateFrom, 
             DateTo = offer.DateTo,
             Seniorities = offer.Seniority.GetValuesFromFlag(),
             WorkLocation = offer.WorkLocation.GetValuesFromFlag(),
             CompanyName = offer.Company.Name,
             CompanyDescription = offer.Company.Description,
             CompanyId = offer.CompanyId,
             Cities = offer.Addresses.Select(address => address.City).ToList(),
             Salaries = salaries,
             Technologies = technologies
         };
    }
    
    public static GetOfferForUpdateDto AsGetOfferForUpdateDto(this Offer offer)
    {
        var salaries = new List<UpdateOfferSalaries>();
        
        foreach (var salary in offer.Salaries)
        {
            salaries.Add(salary.AsUpdateOfferSalaries());
        }

        return new GetOfferForUpdateDto
        { 
            Id = offer.Id,
            Title = offer.Title,
            Description  = offer.Description,
            DateFrom = offer.DateFrom,
            DateTo = offer.DateTo,
            Status = offer.Status,
            Seniorities = offer.Seniority.GetValuesFromFlag(),
            WorkLocations = offer.WorkLocation.GetValuesFromFlag(),
            Salaries = salaries,
            AddressIds = offer.Addresses.Select(address => address.Id).ToList(),
            TechnologyIds = offer.Technologies.Select(technology => technology.Id).ToList()
        };
    }
    
    private static SalaryDto AsDto(this Salary salary)
    {
        return new SalaryDto
        {
            SalaryFrom = salary.SalaryFrom,
            SalaryTo = salary.SalaryTo,
            EmploymentType = salary.EmploymentType
        };
    }
    
    private static TechnologyDto AsDto(this Technology technology)
    {
        return new TechnologyDto
        {
            Name = technology.Name,
            ThumUrl = technology.ThumUrl
        };
    }
    
    private static UpdateOfferSalaries AsUpdateOfferSalaries(this Salary salary)
    {
        return new UpdateOfferSalaries
        (
            salary.SalaryFrom,
            salary.SalaryTo,
            salary.EmploymentType
        );
    }
}