﻿using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using skit.Core.Companies.Repositories;
using skit.Core.Identity.Services;
using skit.Infrastructure.DAL.Companies;
using skit.Infrastructure.DAL.Companies.Repositories;
using skit.Infrastructure.DAL.EF.Context;
using skit.Infrastructure.DAL.Identity.Services;

namespace skit.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

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
        
        return services;
    }
}