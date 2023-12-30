using MediatR;

namespace skit.Application.JobApplications.Queries.GetJobApplication;

public sealed record GetJobApplicationQuery(Guid JobApplicationId) : IRequest<GetJobApplicationResponse>;