using skit.Core.Files.Entities;

namespace skit.Application.Files.Commands.UploadFile;

public sealed record UploadFileResponse(Guid Id, SystemFile File);
