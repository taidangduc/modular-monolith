using BuildingBlocks.Core.Event;
using ModularMonolith.BuildingBlocks.Core.SeedWork;
using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.Preference;

public class PreferenceEventMapper : IEventMapper
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
