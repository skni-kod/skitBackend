using MediatR;
using Microsoft.AspNetCore.Http;
using skit.Core.Files.Enums;

namespace skit.Application.Files.Commands.UploadFile;

public sealed record UploadFilCommand(IFormFile File, FileType Type) : IRequest<UploadFileResponse>;