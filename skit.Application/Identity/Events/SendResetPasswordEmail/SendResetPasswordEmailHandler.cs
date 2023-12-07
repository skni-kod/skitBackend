using MediatR;
using skit.Core.Emails.Services;
using skit.Core.Identity.Services;
using skit.Shared;

namespace skit.Application.Identity.Events.SendResetPasswordEmail;

public sealed class SendResetPasswordEmailHandler : INotificationHandler<SendResetPasswordEmailEvent>
{
    private readonly IEmailSenderService _emailSenderService;
    private readonly IIdentityService _identityService;

    public SendResetPasswordEmailHandler(IEmailSenderService emailSenderService, IIdentityService identityService)
    {
        _emailSenderService = emailSenderService;
        _identityService = identityService;
    }

    public async Task Handle(SendResetPasswordEmailEvent @event, CancellationToken cancellationToken)
    {
        var response = await _identityService.GeneratePasswordResetTokenAsync(@event.Email, cancellationToken);
        
        var link = SetUrl(response.Token, response.UserId);
        var message = "Click link bellow reset your password" + Environment.NewLine + link;
        
        await _emailSenderService.SendEmailAsync(@event.Email, "Reset password", message);
    }
    
    private string SetUrl(string token, Guid userId)
    {
        return $"{Globals.ApplicationUrl}/reset-password?token={token}&userId={userId}";
    }
}