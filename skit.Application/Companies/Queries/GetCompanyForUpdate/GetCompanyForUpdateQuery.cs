using MediatR;

namespace skit.Application.Companies.Queries.GetCompanyForUpdate;

public sealed record GetCompanyForUpdateQuery(Guid CompanyId) : IRequest<GetCompanyForUpdateResponse>;