using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Salaries.Exceptions;

public class NotAllSalariesFoundException : SkitException
{
    public NotAllSalariesFoundException() : base("Not all salaries found")
    {
    }
}