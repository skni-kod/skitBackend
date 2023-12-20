namespace skit.Core.Common.Services;

public interface ICurrentUserService
{
    Guid UserId { get; }
    Guid CompanyId { get; }
    Task<bool> IsEmailConfirmedAsync(CancellationToken cancellationToken);
}