using MediatR;

namespace skit.Application.Salaries.Commands.UpdateSalary;

public sealed record UpdateSalaryCommand(Guid SalaryId, decimal? SalaryFrom, decimal? SalaryTo) : IRequest;