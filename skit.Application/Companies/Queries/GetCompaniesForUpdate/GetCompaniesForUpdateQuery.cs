using MediatR;
using skit.Application.Companies.Queries.GetCompaniesForUpdate.DTO;

namespace skit.Application.Companies.Queries.GetCompaniesForUpdate;

public sealed record GetCompaniesForUpdateQuery(Guid CompanyId) : IRequest<GetCompaniesForUpdateDto>;