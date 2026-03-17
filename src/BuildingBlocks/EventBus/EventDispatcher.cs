using MassTransit;
using ModularMonolith.BuildingBlocks.Core.SeedWork;

namespace ModularMonolith.BuildingBlocks.EventBus;

public class EventDispatcher(IBus bus, IEventMapper eventMapper ) : IEventDispatcher
{
    public async Task DispatchAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(domainEvent, nameof(domainEvent));

        var integrationEvent = eventMapper.MapToIntegrationEvent(domainEvent) 
            ?? throw new InvalidOperationException("Failed to map domain event to integration event.");

        await bus.Publish(integrationEvent, cancellationToken);
    }
}
