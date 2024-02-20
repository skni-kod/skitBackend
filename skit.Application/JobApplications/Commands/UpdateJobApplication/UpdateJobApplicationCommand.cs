using MediatR;
using skit.Shared.Responses;

namespace skit.Application.JobApplications.Commands.UpdateJobApplication;

public record UpdateJobApplicationCommand
    (string FirstName, string SurName, string? Description) : IRequest<CreateOrUpdateResponse>
{
    internal Guid JobApplicationId { get; init; }
}