using System.Linq.Expressions;
using Hangfire;
using Hangfire.Common;
using skit.Core.BackgroundProcessing.Services;

namespace skit.Infrastructure.Common.Services;

public sealed class HangfireService : IHangfireService
{
    private const string RecurringEveryMinute = "* * * * *";
    private const string RecurringEveryDay = "0 0 * * *";
    
    public void ScheduleEveryMinute(string jobId, Expression<Action> methodCall)
    {
        var manager = new RecurringJobManager();
        manager.AddOrUpdate(jobId, Job.FromExpression(methodCall), RecurringEveryMinute);
    }
    
    public void ScheduleEveryDay(string jobId, Expression<Action> methodCall)
    {
        var manager = new RecurringJobManager();
        manager.AddOrUpdate(jobId, Job.FromExpression(methodCall), RecurringEveryDay);
    }

    public void RemoveIfExists(string jobId)
    {
        var manager = new RecurringJobManager();
        manager.RemoveIfExists(jobId);
    }
}