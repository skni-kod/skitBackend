using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using skit.Core.Common.Services;
using skit.Infrastructure.Common.Services;

namespace skit.Infrastructure.Common;

internal static class Extension
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IDateService, DateService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}