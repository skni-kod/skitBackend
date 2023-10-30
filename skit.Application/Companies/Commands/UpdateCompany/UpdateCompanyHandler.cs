using MediatR;
using skit.Core.Companies.Exceptions;
using skit.Core.Companies.Repositories;

namespace skit.Application.Companies.Commands.UpdateCompany;

internal class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository;

    public UpdateCompanyHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }
    
    public async Task Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetAsync(request.CompanyId, cancellationToken);
        if (company is null)
            throw new CompanyNotFoundException("Company does not exist");

        company.ChangeCompanyInformation(request.Description, request.Size, request.Links, request.OwnerId);

        await _companyRepository.UpdateAsync(company, cancellationToken);
    }
}