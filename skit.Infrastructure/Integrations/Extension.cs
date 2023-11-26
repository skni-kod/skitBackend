using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using skit.Core.Emails.Services;
using skit.Infrastructure.Integrations.Emails.Configuration;
using skit.Infrastructure.Integrations.Emails.Services;

namespace skit.Infrastructure.Integrations;

internal static class Extension
{
    public static IServiceCollection AddIntegrations(this IServiceCollection services, IConfiguration configuration)
    {
        var smtpConfig = new SmtpConfig();
        configuration.GetSection("SMTP").Bind(smtpConfig);
        services.AddSingleton(smtpConfig);

        services.AddScoped<IEmailSenderService, EmailSenderService>();

        return services;
    }
}