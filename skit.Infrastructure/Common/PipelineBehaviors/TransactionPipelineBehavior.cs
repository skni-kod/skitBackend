using System.Transactions;
using MediatR;
using skit.Infrastructure.DAL.EF.Context;
using skit.Shared.Requests;

namespace skit.Infrastructure.Common.PipelineBehaviors;

public sealed class TransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : ITransactionalRequest
{
    private readonly EFContext _context;

    public TransactionPipelineBehavior(EFContext context)
    {
        _context = context;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            var response = await next();

            await _context.SaveChangesAsync(cancellationToken);

            scope.Complete();

            return response;
        }
        catch
        {
            // Transaction will be automatically rolled back
            throw;
        }
        finally
        {
            scope.Dispose();
        }
    }
}