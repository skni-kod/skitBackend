using MediatR;
using skit.Core.JobApplications.Exceptions;
using skit.Core.JobApplications.Repositories;
using skit.Shared.Responses;

namespace skit.Application.JobApplications.Commands.UpdateJobApplication;

internal sealed class UpdateJobApplicationHandler : IRequestHandler<UpdateJobApplicationCommand, CreateOrUpdateResponse>
{
    private readonly IJobApplicationRepository _jobApplicationRepository;

    public UpdateJobApplicationHandler(IJobApplicationRepository jobApplicationRepository)
    {
        _jobApplicationRepository = jobApplicationRepository;
    }
    
    public async Task<CreateOrUpdateResponse> Handle(UpdateJobApplicationCommand request, CancellationToken cancellationToken)
    {
        var jobApplication = await _jobApplicationRepository.GetAsync(request.JobApplicationId, cancellationToken) 
                    ?? throw new JobApplicationNotFoundException();

        jobApplication.Update(
            request.FirstName,
            request.SurName,
            request.Description);

        var resultId = await _jobApplicationRepository.UpdateAsync(jobApplication, cancellationToken);

        return new CreateOrUpdateResponse(resultId);
    }
}