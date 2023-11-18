namespace skit.Core.Emails.Sender;

public interface IEmailSenderService
{
    Task SendEmailAsync(string email, string subject, string textBody);
}