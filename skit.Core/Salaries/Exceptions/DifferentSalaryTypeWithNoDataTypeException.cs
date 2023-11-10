using skit.Shared.Abstractions.Exceptions;

namespace skit.Core.Salaries.Exceptions;

public class DifferentSalaryTypeWithNoDataTypeException : SkitException
{
    public DifferentSalaryTypeWithNoDataTypeException() : base("You cannot add other salary type when you add NoData salary type. Firstly delete this type and then add others.")
    {
    }
}