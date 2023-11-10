using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Salaries.Exceptions;

public class DuplicateEmploymentTypeException : SkitException
{
    public DuplicateEmploymentTypeException() : base("You cannot add few salaries for one employment type")
    {
    }
}