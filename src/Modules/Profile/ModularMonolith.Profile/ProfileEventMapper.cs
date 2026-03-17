using BuildingBlocks.Core;
using BuildingBlocks.Core.Event;
using ModularMonolith.BuildingBlocks.Core.SeedWork;
using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.Profile;

public class ProfileEventMapper : IEventMapper
{
    public IIntegrationEvent? MapToIntegrationEvent(DomainEvent @event)
    {
        switch (@event)
        {
            default:
                return null;
        }
    }
}

