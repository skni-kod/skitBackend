using MediatR;

namespace skit.Application.Identity.Commands.ConfirmAccount;

public sealed record ConfirmAccountCommand(Guid UserId, string Token) : IRequest;
