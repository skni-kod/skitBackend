using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using skit.Application.Files.Commands.UploadFile;
using skit.Core.Files.Enums;
using skit.Core.Technologies.Entities;
using skit.Infrastructure.DAL.EF.Context;
using skit.Infrastructure.DAL.EF.Seeder.Technologies.DTO;

namespace skit.Infrastructure.DAL.EF.Seeder.Technologies;

internal static class TechnologiesSeeder
{
    internal static async Task SeedAsync(EFContext context, IMediator mediator, CancellationToken cancellationToken)
    {
        var technologiesInDatabase = await context.Technologies.AsNoTracking().ToListAsync(cancellationToken);

        var technologies = JsonConvert.DeserializeObject<List<TechnologySeedDto>>(await File.ReadAllTextAsync("wwwroot/SeedData/Technologies/technologies.json", cancellationToken));

        if (technologies is null)
            return;

        technologies = technologies.Where(x => !technologiesInDatabase.Any(y => y.Name == x.Name)).ToList();
        
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        
        foreach (var technologyDto in technologies)
        {
            Guid? fileId = null;
            
            if (!string.IsNullOrWhiteSpace(technologyDto.PhotoPath))
            {
                var filePath = $"wwwroot/SeedData/Technologies/Images/{technologyDto.PhotoPath}";
                using var stream = new MemoryStream(await File.ReadAllBytesAsync(filePath, cancellationToken));
                var lastDotPosition = technologyDto.PhotoPath.LastIndexOf('.');
                var formFile = new FormFile(stream, 0, stream.Length, technologyDto.Name, Path.GetFileName(filePath))
                {
                    Headers = new HeaderDictionary(),
                    ContentType =
                        $"image/{technologyDto.PhotoPath.Substring(lastDotPosition + 1)}", // replace with your actual content type
                    ContentDisposition =
                        $"form-data; name=\"{technologyDto.Name}\"; filename=\"{Path.GetFileName(filePath)}\""
                };
            
                var uploadFileResponse = await mediator.Send(new UploadFileCommand(formFile, FileType.Image), cancellationToken);
                fileId = uploadFileResponse.Id;
            }
            
            var technology = Technology.Create(technologyDto.Name, technologyDto.ThumUrl, fileId);
            
            await context.Technologies.AddAsync(technology, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        await transaction.CommitAsync(cancellationToken);
    }
}