using MediatR;
using skit.Core.Common.Services;
using skit.Core.JobApplications.Exceptions;
using skit.Core.JobApplications.Repositories;
using skit.Core.Offers.Exceptions;
using skit.Core.Offers.Repositories;
using skit.Shared.Responses;

namespace skit.Application.JobApplications.Commands.UpdateJobApplication;

internal sealed class UpdateJobApplicationHandler : IRequestHandler<UpdateJobApplicationCommand, CreateOrUpdateResponse>
{
    private readonly IJobApplicationRepository _jobApplicationRepository;
    private readonly IOfferRepository _offerRepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdateJobApplicationHandler(IJobApplicationRepository jobApplicationRepository, IOfferRepository offerRepository, ICurrentUserService currentUserService)
    {
        _jobApplicationRepository = jobApplicationRepository;
        _offerRepository = offerRepository;
        _currentUserService = currentUserService;
    }
    
    public async Task<CreateOrUpdateResponse> Handle(UpdateJobApplicationCommand request, CancellationToken cancellationToken)
    {
        var jobApplication = await _jobApplicationRepository.GetAsync(request.JobApplicationId, cancellationToken) 
                    ?? throw new JobApplicationNotFoundException();

        var offer = await _offerRepository.GetAsync(jobApplication.OfferId, cancellationToken)
                    ?? throw new OfferNotFoundException();

        if (offer.CompanyId != _currentUserService.CompanyId)
            throw new JobApplicationNotFoundException();

        jobApplication.Update(
            request.FirstName,
            request.SurName,
            request.Description);

        var resultId = await _jobApplicationRepository.UpdateAsync(jobApplication, cancellationToken);

        return new CreateOrUpdateResponse(resultId);
    }
}