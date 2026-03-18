using ModularMonolith.BuildingBlocks.Core.SeedWork;
using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.Profile;

public class ProfileEventMapper : IEventMapper
{
    public IntegrationEvent? MapToIntegrationEvent(DomainEvent @event)
    {
        switch (@event)
        {
            default:
                return null;
        }
    }
}

