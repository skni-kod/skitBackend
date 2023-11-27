namespace skit.Infrastructure.Integrations.Emails.Configuration;

public sealed class SmtpConfig
{
    public string SmtpUrl { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpLogin { get; set; }
    public string SmtpPassword { get; set; }
    public string SmtpSenderMail { get; set; }
    public string SmtpSenderName { get; set; }
}