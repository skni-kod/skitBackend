using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using skit.Infrastructure.DAL.EF.Context;
using skit.Infrastructure.DAL.EF.Seeder.Technologies;
using skit.Infrastructure.DAL.EF.Seeder.UserRoles;

namespace skit.Infrastructure.DAL.EF.Seeder;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetService(typeof(EFContext)) as EFContext;
        var roleManager = scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole<Guid>>)) as RoleManager<IdentityRole<Guid>>;
        var mediator = scope.ServiceProvider.GetService(typeof(IMediator)) as IMediator;

        if (context is not null)
        {
            await context.Database.MigrateAsync(cancellationToken);
            
            await UserRolesSeeder.SeedAsync(roleManager, context, cancellationToken);
            await TechnologiesSeeder.SeedAsync(context, mediator, cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}