using MediatR;

namespace skit.Application.Identity.Commands.SignOut;

public sealed record SignOutCommand(string RefreshToken) : IRequest;
