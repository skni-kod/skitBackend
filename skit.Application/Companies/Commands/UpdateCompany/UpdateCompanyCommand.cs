using MediatR;
using skit.Core.Companies.Enums;

namespace skit.Application.Companies.Commands.UpdateCompany;

public sealed record UpdateCompanyCommand(
    string Name,
    string? Description,
    CompanySize Size,
    string? Links) : IRequest
{
    internal Guid CompanyId { get; set; }
}