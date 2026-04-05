using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Blocks.EntityFramework.Interceptors;

public sealed class DispatchDomainEventsInterceptor(IMediator _mediator) : SaveChangesInterceptor
{
    public override Task SaveChangesFailedAsync(DbContextErrorEventData eventData,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return base.SaveChangesFailedAsync(eventData, cancellationToken);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken ctx = new CancellationToken())
    {
        result = await  base.SavedChangesAsync(eventData, result, ctx);
        if (eventData.Context is not null)
        {
            await _mediator.DispatchDomainEventsAsync(eventData.Context, ctx);
        }
        
        return result;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}