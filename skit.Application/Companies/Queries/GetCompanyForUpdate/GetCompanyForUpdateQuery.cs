using MediatR;

namespace skit.Application.Companies.Queries.GetCompanyForUpdate;

public sealed record GetCompanyForUpdateQuery : IRequest<GetCompanyForUpdateResponse>;