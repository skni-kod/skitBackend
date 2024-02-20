using MediatR;

namespace skit.Application.JobApplications.Commands.DeleteJobApplication;

public sealed record DeleteJobApplicationCommand(Guid JobApplicationId) : IRequest;