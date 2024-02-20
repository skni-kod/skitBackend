using skit.Application.JobApplications.Queries.DTO;

namespace skit.Application.JobApplications.Queries.BrowseJobApplications;

public sealed record BrowseJobApplicationsResponse(List<JobApplicationDto> JobApplicationDto);