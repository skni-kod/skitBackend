using skit.Core.JobApplications.Entities;

namespace skit.Core.JobApplications.Repositories;

public interface IJobApplicationRepository
{
    public Task<Guid> AddAsync(JobApplication jobApplication, CancellationToken cancellationToken);
}