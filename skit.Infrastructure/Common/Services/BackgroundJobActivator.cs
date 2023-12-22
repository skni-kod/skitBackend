using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using skit.Application.Offers.Commands.FinishOffers;
using skit.Core.BackgroundProcessing.Services;
using skit.Core.Offers.Repositories;

namespace skit.Infrastructure.Common.Services;

public class BackgroundJobActivator : IHostedService
{
    private const string JobId = "FinishOffers";
    private readonly IServiceProvider _serviceProvider;
    private IMediator _mediator;

    public BackgroundJobActivator(IServiceProvider serviceProvider, IMediator mediator)
    {
        _serviceProvider = serviceProvider;
        _mediator = mediator;
    }
    

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var hangfireService = scope.ServiceProvider.GetRequiredService<IHangfireService>();

            hangfireService.ScheduleEveryDay(JobId, () => _mediator.Send(new FinishOffersCommand(), cancellationToken));
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var hangfireService = scope.ServiceProvider.GetRequiredService<IHangfireService>();

            hangfireService.RemoveIfExists(JobId);
        }
    }
}