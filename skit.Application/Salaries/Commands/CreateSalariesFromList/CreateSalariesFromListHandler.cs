using MediatR;
using skit.Core.Salaries.Entities;
using skit.Core.Salaries.Enums;
using skit.Core.Salaries.Exceptions;
using skit.Core.Salaries.Repositories;

namespace skit.Application.Salaries.Commands.CreateSalariesFromList;

internal sealed class CreateSalariesFromListHandler : IRequestHandler<CreateSalariesFromListCommand, CreateSalariesFromListResponse>
{
    private readonly ISalaryRepository _salaryRepository;

    public CreateSalariesFromListHandler(ISalaryRepository salaryRepository)
    {
        _salaryRepository = salaryRepository;
    }

    public async Task<CreateSalariesFromListResponse> Handle(CreateSalariesFromListCommand request, CancellationToken cancellationToken)
    {
        if (request.Salaries.Count() > 1)
        {
            if (request.Salaries.Any(salary => salary.EmploymentType is null or SalaryEmploymentType.NoData))
            {
                throw new DifferentSalaryTypeWithNoDataTypeException();
            }
        }
                
        var duplicateEmploymentTypes = request.Salaries
            .GroupBy(c => c.EmploymentType)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicateEmploymentTypes.Any())
            throw new DuplicateEmploymentTypeException();

        var salaries = new List<Salary>();

        foreach (var salary in request.Salaries)
        {
            salaries.Add(Salary.Create(salary.SalaryFrom, salary.SalaryTo, salary.EmploymentType, request.OfferId));
        }
        
        await _salaryRepository.AddRangeAsync(salaries, cancellationToken);

        return new CreateSalariesFromListResponse(new List<Guid>(salaries.Select(salary => salary.Id).ToList()));
    }
}