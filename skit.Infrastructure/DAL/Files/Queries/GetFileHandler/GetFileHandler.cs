using MediatR;
using skit.Application.Files.Queries.GetFile;
using skit.Core.Files.Services;
using skit.Infrastructure.DAL.EF.Context;

namespace skit.Infrastructure.DAL.Files.Queries.GetFileHandler;

public sealed class GetFileHandler : IRequestHandler<GetFileQuery, GetFileResponse>
{
    private readonly EFContext _context;
    private readonly IS3StorageService _s3StorageService;

    public GetFileHandler(EFContext context, IS3StorageService s3StorageService)
    {
        _context = context;
        _s3StorageService = s3StorageService;
    }
    
    public async Task<GetFileResponse> Handle(GetFileQuery request, CancellationToken cancellationToken)
    {
        var file = await _context.Files.AsTracking()
            .Where(x => x.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken)
                   ?? throw new FileNotFoundException();
        
        var fileUrl = _s3StorageService.GetFileUrl(file.S3Key, file.Name);

        return new GetFileResponse(fileUrl);
    }
}