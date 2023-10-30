using MediatR;
using skit.Core.Companies.Exceptions;
using skit.Core.Companies.Repositories;

namespace skit.Application.Companies.Commands.DeleteCompany;

internal sealed class DeleteCompanyHandler : IRequestHandler<DeleteCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository;

    public DeleteCompanyHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }
    
    public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetAsync(request.CompanyId, cancellationToken);
        if (company is null)
            throw new CompanyNotFoundException("Company does not exist");
        
        company.SoftDelete();
        
        await _companyRepository.UpdateAsync(company, cancellationToken);
    }
}