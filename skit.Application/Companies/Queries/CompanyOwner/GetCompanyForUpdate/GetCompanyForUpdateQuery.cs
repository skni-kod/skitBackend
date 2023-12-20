using MediatR;

namespace skit.Application.Companies.Queries.CompanyOwner.GetCompanyForUpdate;

public sealed record GetCompanyForUpdateQuery : IRequest<GetCompanyForUpdateResponse>;