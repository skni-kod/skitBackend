using MediatR;

namespace skit.Application.Identity.Commands.ResetPassword;

public sealed record ResetPasswordCommand(string Token, Guid UserId, string Password, string ConfirmedPassword) : IRequest;
