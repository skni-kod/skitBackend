namespace skit.Core.Common.Services;

public interface IDateService
{
    DateTimeOffset CurrentOffsetDate();
    DateTime CurrentDate();
}