using skit.Application.JobApplications.Queries.DTO;

namespace skit.Application.JobApplications.Queries.GetJobApplication;

public sealed record GetJobApplicationResponse(JobApplicationDetailsDto? JobApplicationDto);