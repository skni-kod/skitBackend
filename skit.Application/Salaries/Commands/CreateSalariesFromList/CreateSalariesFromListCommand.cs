using MediatR;
using skit.Application.Salaries.Commands.CreateSalary;

namespace skit.Application.Salaries.Commands.CreateSalariesFromList;

public sealed record CreateSalariesFromListCommand(IEnumerable<CreateSalaryCommand> Salaries) : IRequest<CreateSalariesFromListResponse>
{
    internal Guid OfferId { get; set; }
}