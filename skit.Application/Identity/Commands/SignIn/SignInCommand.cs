using MediatR;
using skit.Core.Identity.DTO;

namespace skit.Application.Identity.Commands.SignIn;

public sealed record SignInCommand(
    string Email,
    string Password) : IRequest<JsonWebToken>;
