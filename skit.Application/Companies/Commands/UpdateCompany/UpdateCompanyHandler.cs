using MediatR;
using skit.Core.Common.Services;
using skit.Core.Companies.Exceptions;
using skit.Core.Companies.Repositories;
using skit.Shared.Responses;

namespace skit.Application.Companies.Commands.UpdateCompany;

internal sealed class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, CreateOrUpdateResponse>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdateCompanyHandler(ICompanyRepository companyRepository, ICurrentUserService currentUserService)
    {
        _companyRepository = companyRepository;
        _currentUserService = currentUserService;
    }
    
    public async Task<CreateOrUpdateResponse> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetAsync(_currentUserService.CompanyId, cancellationToken)
            ?? throw new CompanyNotFoundException();

        company.Update(command.Name, command.Description, command.Size, command.Links);

        var result = await _companyRepository.UpdateAsync(company, cancellationToken);

        return new CreateOrUpdateResponse(result);
    }
}