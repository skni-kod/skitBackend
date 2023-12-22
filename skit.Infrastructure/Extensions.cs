using System.Reflection;
using FluentValidation;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using skit.Infrastructure.Common;
using skit.Infrastructure.DAL;
using skit.Infrastructure.Integrations;

namespace skit.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(configuration.GetConnectionString("DatabaseConnection")));
        services.AddHangfireServer();

        services.AddCommonInfrastructure();
        services.AddDal(configuration);
        services.AddIntegrations(configuration);
      
        return services;
    }
}