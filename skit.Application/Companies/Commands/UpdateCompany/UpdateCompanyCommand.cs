using MediatR;
using skit.Core.Companies.Enums;
using skit.Core.Identity.Entities;

namespace skit.Application.Companies.Commands.UpdateCompany;

public record UpdateCompanyCommand(
    string? Description,
    CompanySize Size,
    string? Links,
    Guid OwnerId) : IRequest
{
    internal Guid CompanyId { get; set; }
}