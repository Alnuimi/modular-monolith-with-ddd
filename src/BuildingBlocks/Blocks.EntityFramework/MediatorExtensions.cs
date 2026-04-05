using Blocks.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blocks.EntityFramework;

public static  class MediatorExtensions
{
    public static async Task<int> DispatchDomainEventsAsync(this IMediator mediator, DbContext dbcontext, CancellationToken ct = default)
    {
        var domainAggregates = dbcontext.ChangeTracker.Entries<IAggregateRoot>();

        var domainEvents = domainAggregates
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();
        
        domainAggregates
            .ToList()
            .ForEach(domainAggregate => domainAggregate.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent, ct);
        }
        
        return domainEvents.Count;  
    }
}