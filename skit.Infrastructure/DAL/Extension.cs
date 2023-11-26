﻿using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using skit.Core.Companies.Repositories;
using skit.Core.Identity.Services;
using skit.Infrastructure.DAL.Companies.Repositories;
using skit.Infrastructure.DAL.EF.Context;
using skit.Infrastructure.DAL.Identity.Services;
using skit.Infrastructure.Integrations.Emails.Configuration;

namespace skit.Infrastructure.DAL;

internal static class Extension
{
    public static IServiceCollection AddDal(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EFContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DatabaseConnection")!,
                opt =>
                {
                    opt.CommandTimeout(30);
                    opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }).LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
        });
        
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ITokenService, TokenService>();
        
        return services;
    }
}