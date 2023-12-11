using MediatR;

namespace skit.Application.Identity.Events.SendConfirmAccountEmail;

public sealed record SendConfirmAccountEmailEvent(Guid? UserId = null) : INotification;
