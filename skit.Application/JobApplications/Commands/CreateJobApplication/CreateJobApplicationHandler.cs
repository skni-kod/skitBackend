using MediatR;
using skit.Core.JobApplications.Entities;
using skit.Core.JobApplications.Repositories;
using skit.Core.Offers.Exceptions;
using skit.Core.Offers.Repositories;
using skit.Shared.Responses;

namespace skit.Application.JobApplications.Commands.CreateJobApplication;

internal sealed class CreateJobApplicationHandler : IRequestHandler<CreateJobApplicationCommand, CreateOrUpdateResponse>
{
    private readonly IOfferRepository _offerRepository;
    private readonly IJobApplicationRepository _jobApplicationRepository;

    public CreateJobApplicationHandler(IOfferRepository offerRepository, IJobApplicationRepository jobApplicationRepository)
    {
        _offerRepository = offerRepository;
        _jobApplicationRepository = jobApplicationRepository;
    }

    public async Task<CreateOrUpdateResponse> Handle(CreateJobApplicationCommand request, CancellationToken cancellationToken)
    {
        var offer = await _offerRepository.GetAsync(request.OfferId, cancellationToken)
                    ?? throw new OfferNotFoundException();

        var jobApplication = JobApplication.Create(
            request.FirstName,
            request.SurName,
            request.Description,
            offer.Id);

        var entityId = await _jobApplicationRepository.AddAsync(jobApplication, cancellationToken);

        return new CreateOrUpdateResponse(entityId);
    }
}