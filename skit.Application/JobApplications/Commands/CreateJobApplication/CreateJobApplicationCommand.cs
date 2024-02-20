using MediatR;
using skit.Shared.Responses;

namespace skit.Application.JobApplications.Commands.CreateJobApplication;

public sealed record CreateJobApplicationCommand(
    Guid OfferId,
    string FirstName,
    string SurName,
    string? Description) : IRequest<CreateOrUpdateResponse>;