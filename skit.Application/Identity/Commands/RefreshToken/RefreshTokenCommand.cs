using MediatR;
using skit.Core.Identity.DTO;

namespace skit.Application.Identity.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string? RefreshToken) : IRequest<JsonWebToken>;
