using MediatR;
using skit.Application.Salaries.Commands.UpdateSalary;

namespace skit.Application.Salaries.Commands.UpdateSalariesFromList;

public sealed record UpdateSalariesFromListCommand(IEnumerable<UpdateSalaryCommand> Salaries) : IRequest
{
    internal Guid OfferId { get; set; }
}