using MediatR;
using skit.Core.Common.Services;
using skit.Core.Emails.Services;
using skit.Core.Identity.Exceptions;
using skit.Core.Identity.Services;
using skit.Shared;

namespace skit.Application.Identity.Events.SendConfirmAccountEmail;

public sealed class SendConfirmAccountEmailHandler : INotificationHandler<SendConfirmAccountEmailEvent>
{
    private readonly IIdentityService _identityService;
    private readonly IEmailSenderService _emailSenderService;
    private readonly ICurrentUserService _currentUserService;

    public SendConfirmAccountEmailHandler(IIdentityService identityService, IEmailSenderService emailSenderService, ICurrentUserService currentUserService)
    {
        _identityService = identityService;
        _emailSenderService = emailSenderService;
        _currentUserService = currentUserService;
    }
    
    public async Task Handle(SendConfirmAccountEmailEvent @event, CancellationToken cancellationToken)
    {
        var userId = @event.UserId ?? _currentUserService.UserId;

        var user = await _identityService.GetAsync(userId, cancellationToken)
                   ?? throw new UserNotFoundException();

        var token = await _identityService.GenerateEmailConfirmationTokenAsync(user, cancellationToken);
        
        var link = SetUrl(user.Id, token);
        var message = "Click link bellow to activate your account" + Environment.NewLine + link;
        
        await _emailSenderService.SendEmailAsync(user.Email!, "Activate your account", message);

    }
    
    private string SetUrl(Guid userId, string token)
    {
        return $"{Globals.ApplicationUrl}/confirm-account?token={token}&userId={userId}";
    }
}