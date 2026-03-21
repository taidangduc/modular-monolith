using ModularMonolith.BuildingBlocks.Core.SeedWork;
using ModularMonolith.BuildingBlocks.EventBus;
using ModularMonolith.Identity.Domain.Events;
using ModularMonolith.Identity.IntegrationEvents.Events;

namespace ModularMonolith.Identity;

public sealed class IdentityEventMapper : IEventMapper
{
    public IntegrationEvent? MapToIntegrationEvent(DomainEvent @event)
    {
        return @event switch
        {
            UserCreatedEvent e => new UserCreatedIntegrationEvent(e.UserId, e.Name, e.Email),
            _ => null
        };
    }
}
