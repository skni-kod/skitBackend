using MediatR;

namespace skit.Application.Companies.Commands.DeleteCompany;

public record DeleteCompanyCommand(Guid CompanyId) : IRequest;