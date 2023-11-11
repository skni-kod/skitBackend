using MediatR;
using skit.Core.Common.Services;
using skit.Core.Companies.Exceptions;
using skit.Core.Companies.Repositories;

namespace skit.Application.Companies.Commands.DeleteCompany;

internal sealed class DeleteCompanyHandler : IRequestHandler<DeleteCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ICurrentUserService _currentUserService;

    public DeleteCompanyHandler(ICompanyRepository companyRepository, ICurrentUserService currentUserService)
    {
        _companyRepository = companyRepository;
        _currentUserService = currentUserService;
    }
    
    public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetAsync(_currentUserService.CompanyId, cancellationToken) 
                      ?? throw new CompanyNotFoundException();
        
        await _companyRepository.DeleteAsync(company, cancellationToken);
    }
}