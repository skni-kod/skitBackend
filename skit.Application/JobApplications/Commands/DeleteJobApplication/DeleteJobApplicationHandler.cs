using MediatR;
using skit.Core.Common.Services;
using skit.Core.JobApplications.Exceptions;
using skit.Core.JobApplications.Repositories;
using skit.Core.Offers.Exceptions;
using skit.Core.Offers.Repositories;

namespace skit.Application.JobApplications.Commands.DeleteJobApplication;

internal sealed class DeleteJobApplicationHandler : IRequestHandler<DeleteJobApplicationCommand>
{
    private readonly IJobApplicationRepository _jobApplicationRepository;
    private readonly IOfferRepository _offerRepository;
    private readonly ICurrentUserService _currentUserService;

    public DeleteJobApplicationHandler(IJobApplicationRepository jobApplicationRepository, IOfferRepository offerRepository, ICurrentUserService currentUserService)
    {
        _jobApplicationRepository = jobApplicationRepository;
        _offerRepository = offerRepository;
        _currentUserService = currentUserService;
    }

    public async Task Handle(DeleteJobApplicationCommand request, CancellationToken cancellationToken)
    {
        var jobApplication = await _jobApplicationRepository.GetAsync(request.JobApplicationId, cancellationToken)
                    ?? throw new JobApplicationNotFoundException();
        
        var offer = await _offerRepository.GetAsync(jobApplication.OfferId, cancellationToken)
                    ?? throw new OfferNotFoundException();

        if (offer.CompanyId != _currentUserService.CompanyId)
            throw new JobApplicationNotFoundException();

        await _jobApplicationRepository.DeleteAsync(jobApplication, cancellationToken);
    }
}