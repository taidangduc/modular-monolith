using ModularMonolith.BuildingBlocks.Core.SeedWork;
using ModularMonolith.BuildingBlocks.EventBus;
using ModularMonolith.Profile.Domain.Events;
using ModularMonolith.Profile.IntegrationEvents.Events;

namespace ModularMonolith.Profile;

public class ProfileEventMapper : IEventMapper
{
    public IntegrationEvent? MapToIntegrationEvent(DomainEvent @event)
    {
        return @event switch
        {
            ProfileCreatedEvent e => new ProfileCreatedIntegrationEvent(e.UserId, e.Name, e.Email),
            ProfileUpdatedEvent e => new ProfileUpdatedIntegrationEvent(e.UserId, e.Name, e.Email),
            _ => null
        };
    }
}

