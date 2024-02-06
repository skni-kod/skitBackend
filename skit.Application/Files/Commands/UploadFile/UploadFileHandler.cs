using MediatR;
using Microsoft.AspNetCore.Http;
using skit.Core.Files.Entities;
using skit.Core.Files.Enums;
using skit.Core.Files.Repositories;
using skit.Core.Files.Services;

namespace skit.Application.Files.Commands.UploadFile;

public sealed class UploadFileHandler : IRequestHandler<UploadFileCommand, UploadFileResponse>
{
    private readonly IFileRepository _fileRepository;
    private readonly IS3StorageService _s3StorageService;

    public UploadFileHandler(IFileRepository fileRepository, IS3StorageService s3StorageService)
    {
        _fileRepository = fileRepository;
        _s3StorageService = s3StorageService;
    }
    
    public async Task<UploadFileResponse> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var s3Key = await _s3StorageService.UploadFileAsync(request.File, cancellationToken);

        var file = SystemFile.Create(request.File.FileName, request.Type, request.File.Length, s3Key, request.File.ContentType);

        var result = await _fileRepository.AddAsync(file, cancellationToken);

        return new UploadFileResponse(result, file);
    }
}
