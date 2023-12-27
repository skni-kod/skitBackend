using MediatR;
using skit.Shared.Responses;

namespace skit.Application.JobApplications.Commands.CreateJobApplication;

public sealed record CreateJobApplicationCommand(
    string FirstName,
    string SurName,
    string? Description) : IRequest<CreateOrUpdateResponse>
{
    internal Guid OfferId { get; init; }
}