namespace skit.Application.JobApplications.Queries.DTO;

public sealed class JobApplicationDetailsDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string SurName { get; set; }
    public string? Description { get; set; }
}