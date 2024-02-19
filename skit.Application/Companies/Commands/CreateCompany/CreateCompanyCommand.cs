using MediatR;
using skit.Core.Companies.Enums;
using skit.Core.Identity.DTO;
using skit.Shared.Requests;
using skit.Shared.Responses;

namespace skit.Application.Companies.Commands.CreateCompany;

public sealed record CreateCompanyCommand(
    string Name,
    string? Description,
    CompanySize Size,
    string? Links) : IRequest<JsonWebToken>, ITransactionalRequest;
