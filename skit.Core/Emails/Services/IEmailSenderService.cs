namespace skit.Core.Emails.Services;

public interface IEmailSenderService
{
    Task SendEmailAsync(string email, string subject, string textBody);
}