
using BuildingBlocks.Core.Event;
using ModularMonolith.BuildingBlocks.Core.SeedWork;
using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.Identity;

public sealed class IdentityEventMapper : IEventMapper
{
    public IIntegrationEvent? MapToIntegrationEvent(DomainEvent @event)
    {
        return @event switch
        {
            _ => null
        };
    }
}
