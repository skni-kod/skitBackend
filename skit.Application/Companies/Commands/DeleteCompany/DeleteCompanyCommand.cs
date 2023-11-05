using MediatR;

namespace skit.Application.Companies.Commands.DeleteCompany;

public sealed record DeleteCompanyCommand(Guid CompanyId) : IRequest;