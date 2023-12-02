using MailKit.Net.Smtp;
using MimeKit;
using skit.Core.Emails.Services;
using skit.Infrastructure.Integrations.Emails.Configuration;

namespace skit.Infrastructure.Integrations.Emails.Services;

public sealed class EmailSenderService : IEmailSenderService
{
    private readonly SmtpConfig _smtpConfig;

    public EmailSenderService(SmtpConfig smtpConfig)
    {
        _smtpConfig = smtpConfig;
    }
    
    public async Task SendEmailAsync(string email, string subject, string textBody)
    {
        var emailMessage = CreateEmailMessage(email, subject, textBody, _smtpConfig);
        using var client = new SmtpClient();
        {
            await client.ConnectAsync(_smtpConfig.SmtpUrl, _smtpConfig.SmtpPort, false);
            await client.AuthenticateAsync(_smtpConfig.SmtpLogin, _smtpConfig.SmtpPassword);
            var status = await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
    
    private MimeMessage CreateEmailMessage(string email, string subject, string textBody, SmtpConfig smtpConfig)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(smtpConfig.SmtpSenderName, smtpConfig.SmtpSenderMail));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;
        
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.TextBody = textBody;
        emailMessage.Body = bodyBuilder.ToMessageBody();

        return emailMessage;
    }
}