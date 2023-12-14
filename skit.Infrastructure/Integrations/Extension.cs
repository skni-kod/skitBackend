using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using skit.Core.Emails.Services;
using skit.Core.Files.Services;
using skit.Infrastructure.Integrations.Emails.Configuration;
using skit.Infrastructure.Integrations.Emails.Services;
using skit.Infrastructure.Integrations.FileStorage.Configuration;
using skit.Infrastructure.Integrations.FileStorage.Services;

namespace skit.Infrastructure.Integrations;

internal static class Extension
{
    public static IServiceCollection AddIntegrations(this IServiceCollection services, IConfiguration configuration)
    {
        var smtpConfig = new SmtpConfig();
        configuration.GetSection("SMTP").Bind(smtpConfig);
        services.AddSingleton(smtpConfig);

        services.AddScoped<IEmailSenderService, EmailSenderService>();

        var s3Config = new S3Config();
        configuration.GetSection("S3Service").Bind(s3Config);
        services.AddSingleton(s3Config);
        
        services.AddScoped<IS3StorageService, S3StorageService>();

        return services;
    }
}