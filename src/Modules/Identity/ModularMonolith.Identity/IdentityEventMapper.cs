using ModularMonolith.BuildingBlocks.Core.SeedWork;
using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.Identity;

public sealed class IdentityEventMapper : IEventMapper
{
    public IntegrationEvent? MapToIntegrationEvent(DomainEvent @event)
    {
        return @event switch
        {
             _ => throw new ArgumentNullException(nameof(@event), $"No mapping found for event type {@event.GetType().FullName}")
        };
    }
}
