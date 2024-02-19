using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.JobApplications.Exceptions;

public sealed class JobApplicationNotFoundException : SkitException
{
    public JobApplicationNotFoundException() : base("JobApplication not found") { }
}