using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using skit.Core.Addresses.Repositories;
using skit.Core.Companies.Repositories;
using skit.Core.Files.Repositories;
using skit.Core.Identity.Services;
using skit.Core.Offers.Repositories;
using skit.Core.Technologies.Repositories;
using skit.Infrastructure.DAL.Addresses.Repositories;
using skit.Infrastructure.DAL.Companies.Repositories;
using skit.Infrastructure.DAL.EF.Context;
using skit.Infrastructure.DAL.EF.Seeder;
using skit.Infrastructure.DAL.Files.Repositories;
using skit.Infrastructure.DAL.Identity.Services;
using skit.Infrastructure.DAL.Offers.Repositories;
using skit.Infrastructure.DAL.Technologies.Repositories;

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

        services.AddHostedService<DatabaseInitializer>();
        
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IOfferRepository, OfferRepository>();
        services.AddScoped<ITechnologyRepository, TechnologyRepository>();
        services.AddScoped<IFileRepository, FileRepository>();
        
        return services;
    }
}