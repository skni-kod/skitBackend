using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Salaries.Exceptions;

public class SalaryDoesNotExistInOfferException : SkitException
{
    public SalaryDoesNotExistInOfferException() : base("Salary does not exist in offer exception")
    {
    }
}