namespace skit.Application.Files.Queries.GetFile;

public sealed record GetFileResponse(MemoryStream Content, string ContentType, string FileName);
