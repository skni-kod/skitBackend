using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using skit.Core.Common.DTO;
using skit.Core.Common.Extensions;
using skit.Core.Companies.Enums;
using skit.Core.Offers.Enums;
using skit.Core.Salaries.Enums;

namespace skit.API.Controllers.Areas.Public;

[AllowAnonymous]
[Route($"{Endpoints.BaseUrl}/enums")]
public sealed class P_EnumsController : BaseController
{
    private readonly IMediator _mediator;

    public P_EnumsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public ActionResult<List<BaseEnum>> asdf()
        => EnumExtensions.GetValues<CompanySize>();
    
    [HttpGet("company-size")]
    public ActionResult<List<BaseEnum>> GetCompanySizeEnum()
        => EnumExtensions.GetValues<CompanySize>();
    
    [HttpGet("offer-seniority")]
    public ActionResult<List<BaseEnum>> GetOfferSeniorityEnum()
        => EnumExtensions.GetValues<OfferSeniority>();
    
    [HttpGet("offer-status")]
    public ActionResult<List<BaseEnum>> GetOfferStatusEnum()
        => EnumExtensions.GetValues<OfferStatus>();
    
    [HttpGet("offer-work-location")]
    public ActionResult<List<BaseEnum>> GetOfferWorkLocationEnum()
        => EnumExtensions.GetValues<OfferWorkLocation>();
    
    [HttpGet("salary-employment-type")]
    public ActionResult<List<BaseEnum>> GetSalaryEmploymentTypeEnum()
        => EnumExtensions.GetValues<SalaryEmploymentType>();
}