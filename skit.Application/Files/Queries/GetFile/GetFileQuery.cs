using MediatR;

namespace skit.Application.Files.Queries.GetFile;

public sealed record GetFileQuery(Guid Id) : IRequest<GetFileResponse>;
