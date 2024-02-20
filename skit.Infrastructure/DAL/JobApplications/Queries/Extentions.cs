using skit.Application.JobApplications.Queries.DTO;
using skit.Core.JobApplications.Entities;

namespace skit.Infrastructure.DAL.JobApplications.Queries;

internal static class Extensions
{
    public static JobApplicationDto AsDto(this JobApplication jobApplication)
    {
        return new JobApplicationDto
        {
            Id = jobApplication.Id,
            FirstName = jobApplication.FirstName,
            SurName = jobApplication.SurName
        };
    }
    
    public static JobApplicationDetailsDto AsDetailsDto(this JobApplication jobApplication)
    {
        return new JobApplicationDetailsDto
        {
            Id = jobApplication.Id,
            FirstName = jobApplication.FirstName,
            SurName = jobApplication.SurName,
            Description = jobApplication.Description
        };
    }
}