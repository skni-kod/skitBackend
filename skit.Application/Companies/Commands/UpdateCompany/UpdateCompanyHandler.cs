using MediatR;
using skit.Core.Common.Services;
using skit.Core.Companies.Exceptions;
using skit.Core.Companies.Repositories;

namespace skit.Application.Companies.Commands.UpdateCompany;

internal sealed class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdateCompanyHandler(ICompanyRepository companyRepository, ICurrentUserService currentUserService)
    {
        _companyRepository = companyRepository;
        _currentUserService = currentUserService;
    }
    
    public async Task Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetAsync(_currentUserService.CompanyId, cancellationToken)
            ?? throw new CompanyNotFoundException();

        company.Update(command.Name, command.Description, command.Size, command.Links);

        await _companyRepository.UpdateAsync(company, cancellationToken);
    }
}