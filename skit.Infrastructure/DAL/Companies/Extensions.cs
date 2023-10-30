using Microsoft.Extensions.DependencyInjection;
using skit.Core.Companies.Repositories;
using skit.Infrastructure.DAL.Companies.Repositories;

namespace skit.Infrastructure.DAL.Companies;

internal static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICompanyRepository, CompanyRepository>();

        return services;
    }
}