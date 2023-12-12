using MediatR;

namespace skit.Application.Identity.Events.SendResetPasswordEmail;

public sealed record SendResetPasswordEmailEvent(string Email) : INotification;
