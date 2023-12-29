using MediatR;
using Microsoft.Extensions.DependencyInjection;
using skit.Core.BackgroundProcessing.Services;
using skit.Core.Common.Services;
using skit.Infrastructure.Common.PipelineBehaviors;
using skit.Infrastructure.Common.Services;

namespace skit.Infrastructure.Common;

internal static class Extension
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>));
        
        services.AddScoped<IDateService, DateService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IHangfireService, HangfireService>();
        services.AddHostedService<BackgroundJobActivator>();
        
        return services;
    }
}