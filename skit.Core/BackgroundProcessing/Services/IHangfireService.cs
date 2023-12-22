using System.Linq.Expressions;

namespace skit.Core.BackgroundProcessing.Services;

public interface IHangfireService
{ 
    void ScheduleEveryMinute(string jobId, Expression<Action> methodCall);
    void ScheduleEveryDay(string jobId, Expression<Action> methodCall);
    void RemoveIfExists(string jobId);
}