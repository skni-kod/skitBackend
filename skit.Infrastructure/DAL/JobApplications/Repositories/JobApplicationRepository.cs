using skit.Core.JobApplications.Entities;
using skit.Core.JobApplications.Repositories;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.JobApplications.Repositories;

internal sealed class JobApplicationRepository : IJobApplicationRepository
{
    private readonly EFContext _context;
    private readonly DbSet<JobApplication> _jobApplications;

    public JobApplicationRepository(EFContext context)
    {
        _context = context;
        _jobApplications = context.JobApplications;
    }

    public async Task<Guid> AddAsync(JobApplication jobApplication, CancellationToken cancellationToken)
    {
        var result = await _jobApplications.AddAsync(jobApplication, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }
}