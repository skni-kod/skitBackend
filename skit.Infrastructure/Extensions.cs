using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using skit.Core.Addresses.Repositories;
using skit.Core.Common.Services;
using skit.Core.Companies.Repositories;
using skit.Core.Identity.Services;
using skit.Infrastructure.Common;
using skit.Infrastructure.Common.Services;
using skit.Infrastructure.DAL;
using skit.Infrastructure.DAL.Companies;
using skit.Infrastructure.DAL.Companies.Repositories;
using skit.Infrastructure.DAL.EF.Context;
using skit.Infrastructure.DAL.Identity.Services;
using skit.Infrastructure.Integrations;
using skit.Infrastructure.Integrations.Emails.Configuration;

namespace skit.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddCommonInfrastructure();
        services.AddDal(configuration);
        services.AddIntegrations(configuration);
      
        return services;
    }
}