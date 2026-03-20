using System.Collections.Immutable;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ModularMonolith.BuildingBlocks.Core.SeedWork;

namespace ModularMonolith.BuildingBlocks.EFCore;

public class DispatchDomainEventInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _publisher;

    public DispatchDomainEventInterceptor(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context == null){
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        var domainEntities = context.ChangeTracker
            .Entries<IHasDomainEvent>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Count != 0)
            .ToImmutableList();

        await DispatchAndClearEvents(domainEntities);

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }


    private async Task DispatchAndClearEvents(IEnumerable<IHasDomainEvent> domainEntities)
    {
        foreach (var entity in domainEntities)
        {
            if (entity is not HasDomainEvent hasDomainEvent)
            {
                continue;
            }

            DomainEvent[] events = hasDomainEvent.DomainEvents.ToArray();
            hasDomainEvent.ClearDomainEvents();

            foreach (var domainEvent in events)
            {
                await _publisher.Publish(domainEvent);
            }
        }
    }
}