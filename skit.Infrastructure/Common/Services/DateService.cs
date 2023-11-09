using skit.Core.Common.Services;

namespace skit.Infrastructure.Common.Services;

public sealed class DateService : IDateService
{
    public DateTimeOffset CurrentOffsetDate() => DateTimeOffset.UtcNow;
    public DateTime CurrentDate() => DateTime.Now;
    
}