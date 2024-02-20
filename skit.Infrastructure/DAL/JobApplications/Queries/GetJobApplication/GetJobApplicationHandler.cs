using MediatR;
using skit.Application.JobApplications.Queries.GetJobApplication;
using skit.Core.Common.Services;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.JobApplications.Queries.GetJobApplication;

internal sealed class GetJobApplicationHandler : IRequestHandler<GetJobApplicationQuery, GetJobApplicationResponse?>
{
    private readonly EFContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetJobApplicationHandler(EFContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<GetJobApplicationResponse?> Handle(GetJobApplicationQuery request, CancellationToken cancellationToken)
    {
        var jobApplication = await _context.JobApplications.AsNoTracking()
            .Include(jobApplication => jobApplication.Offer)
            .Where(jobApplication => jobApplication.Offer.CompanyId == _currentUserService.CompanyId)
            .Where(jobApplication => jobApplication.Id == request.JobApplicationId)
            .Select(jobApplication => new GetJobApplicationResponse(jobApplication.AsDetailsDto()))
            .SingleOrDefaultAsync(cancellationToken);

        return jobApplication;
    }
}