using BuildingBlocks.Core;
using BuildingBlocks.Core.Event;

namespace ModularMonolith.Preference;

public class PreferenceEventMapper : IEventMapper
{
    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent @event)
    {
        switch (@event)
        {
            default:
                return null;
        }
    }

    public IInternalCommand? MapToInternalCommand(IDomainEvent @event)
    {
        switch (@event)
        {
            default:
                return null;
        }
    }
}
