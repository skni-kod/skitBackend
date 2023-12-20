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
        services.Configure<SmtpConfig>(configuration.GetSection("SMTP"));
        services.Configure<S3Config>(configuration.GetSection("S3Service"));

        services.AddScoped<IEmailSenderService, EmailSenderService>();
        services.AddScoped<IS3StorageService, S3StorageService>();

        return services;
    }
}