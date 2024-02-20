using skit.Application.JobApplications.Queries.DTO;
using skit.Core.JobApplications.Entities;

namespace skit.Infrastructure.DAL.JobApplications.Queries;

internal static class Extensions
{
    public static JobApplicationDto AsDetailsDto(this JobApplication jobApplication)
    {
        return new JobApplicationDto
        {
            FirstName = jobApplication.FirstName,
            SurName = jobApplication.SurName,
            Description = jobApplication.Description
        };
    }
}