using MediatR;
using skit.Application.JobApplications.Queries.BrowseJobApplications;
using skit.Core.Common.Services;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.JobApplications.Queries.BrowseJobApplications;

public sealed class BrowseJobApplicationsHandler : IRequestHandler<BrowseJobApplicationsQuery, BrowseJobApplicationsResponse>
{
    private readonly EFContext _context;
    private readonly ICurrentUserService _currentUserService;

    public BrowseJobApplicationsHandler(EFContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    
    public async Task<BrowseJobApplicationsResponse> Handle(BrowseJobApplicationsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.JobApplications.AsNoTracking()
            .Include(jobApplications => jobApplications.Offer)
            .Where(jobApplications => jobApplications.Offer.CompanyId == _currentUserService.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.Search))
            query = query
                .Where(x => EFCore.Functions.ILike(x.FirstName, $"%{request.Search}%") ||
                            EFCore.Functions.ILike(x.SurName, $"%{request.Search}%"));

        var jobApplications = await query
            .Select(x => x.AsDto())
            .ToListAsync(cancellationToken);

        return new BrowseJobApplicationsResponse(jobApplications);
    }
}