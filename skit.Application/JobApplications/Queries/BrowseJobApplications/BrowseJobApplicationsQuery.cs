using MediatR;

namespace skit.Application.JobApplications.Queries.BrowseJobApplications;

public sealed record BrowseJobApplicationsQuery(string? Search) : IRequest<BrowseJobApplicationsResponse>;