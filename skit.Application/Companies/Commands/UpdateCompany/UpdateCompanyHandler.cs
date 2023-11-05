using MediatR;
using skit.Core.Companies.Exceptions;
using skit.Core.Companies.Repositories;

namespace skit.Application.Companies.Commands.UpdateCompany;

internal sealed class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository;

    public UpdateCompanyHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }
    
    public async Task Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetAsync(command.CompanyId, cancellationToken)
            ?? throw new CompanyNotFoundException();

        company.Update(command.Name, command.Description, command.Size, command.Links);

        await _companyRepository.UpdateAsync(company, cancellationToken);
    }
}