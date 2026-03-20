using ModularMonolith.BuildingBlocks.Core.SeedWork;
using ModularMonolith.BuildingBlocks.EventBus;
using ModularMonolith.Preference.Domain.Events;
using ModularMonolith.Preference.IntegrationEvents.Events;

namespace ModularMonolith.Preference;

public class PreferenceEventMapper : IEventMapper
{
    public IntegrationEvent? MapToIntegrationEvent(DomainEvent @event)
    {
        return @event switch
        {
            PreferenceCreatedEvent e => new PreferenceCreatedIntegrationEvent(e.UserId, e.Channel, e.IsOptOut),

            PreferenceUpdatedEvent e => new PreferenceUpdatedIntegrationEvent(e.UserId, e.Channel, e.IsOptOut),

            _ => throw new ArgumentNullException(nameof(@event), $"No mapping found for event type {@event.GetType().FullName}")
        };
    }
}
