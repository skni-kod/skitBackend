using MediatR;
using skit.Core.Salaries.Enums;

namespace skit.Application.Salaries.Commands.CreateSalary;

public sealed record CreateSalaryCommand(decimal? SalaryFrom, decimal? SalaryTo, SalaryEmploymentType? EmploymentType) : IRequest<CreateSalaryResponse>;