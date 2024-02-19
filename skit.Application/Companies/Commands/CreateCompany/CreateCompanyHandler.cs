using MediatR;
using skit.Core.Common.Services;
using skit.Core.Companies.Entities;
using skit.Core.Companies.Exceptions;
using skit.Core.Companies.Repositories;
using skit.Core.Identity.DTO;
using skit.Core.Identity.Services;
using skit.Shared.Responses;

namespace skit.Application.Companies.Commands.CreateCompany;

public sealed class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, JsonWebToken>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public CreateCompanyHandler(ICompanyRepository companyRepository, ICurrentUserService currentUserService, IIdentityService identityService)
    {
        _companyRepository = companyRepository;
        _currentUserService = currentUserService;
        _identityService = identityService;
    }
    
    public async Task<JsonWebToken> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        if (_currentUserService.CompanyId != Guid.Empty)
            throw new CompanyExistsException();
        
        if (await _companyRepository.AnyAsync(request.Name, cancellationToken))
            throw new CompanyWithNameExistsException();
        
        var userId = _currentUserService.UserId;

        var company = Company.Create(
            request.Name,
            request.Description,
            request.Size,
            request.Links,
            userId);

        var companyId = await _companyRepository.CreateAsync(company, cancellationToken);

        var jwt = await _identityService.AddCompanyToUserAsync(userId, companyId, cancellationToken);

        return jwt;
    }
}