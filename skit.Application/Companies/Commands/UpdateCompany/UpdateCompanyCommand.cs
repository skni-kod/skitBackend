using MediatR;
using skit.Core.Companies.Enums;
using skit.Shared.Responses;

namespace skit.Application.Companies.Commands.UpdateCompany;

public sealed record UpdateCompanyCommand(
    string Name,
    string? Description,
    CompanySize Size,
    string? Links) : IRequest<CreateOrUpdateResponse>
{
    internal Guid CompanyId { get; set; }
}