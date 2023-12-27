using MediatR;
using skit.Core.Identity.DTO;

namespace skit.Application.Identity.Commands.SignInGoogle;

public sealed record SignInGoogleCommand : IRequest<JsonWebToken>;
