using MediatR;
using skit.Application.Files.Commands.UploadFile;
using skit.Core.Common.Services;
using skit.Core.Companies.Exceptions;
using skit.Core.Companies.Repositories;
using skit.Core.Files.Enums;

namespace skit.Application.Companies.Commands.UploadCompanyImage;

public sealed class UploadCompanyImageHandler : IRequestHandler<UploadCompanyImageCommand>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public UploadCompanyImageHandler(ICompanyRepository companyRepository, IMediator mediator, ICurrentUserService currentUserService)
    {
        _companyRepository = companyRepository;
        _mediator = mediator;
        _currentUserService = currentUserService;
    }
    
    public async Task Handle(UploadCompanyImageCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetAsync(_currentUserService.CompanyId, cancellationToken)
                      ?? throw new CompanyNotFoundException();
        
        var fileResult = await _mediator.Send(new UploadFileCommand(request.File, FileType.Image), cancellationToken);
        
        company.SetImage(fileResult.Id);
        await _companyRepository.UpdateAsync(company, cancellationToken);
    }
}