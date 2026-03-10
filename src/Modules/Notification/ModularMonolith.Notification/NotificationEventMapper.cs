using BuildingBlocks.Core;
using BuildingBlocks.Core.Event;

namespace ModularMonolith.Notification;
public sealed class NotificationEventMapper : IEventMapper
{
    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent @event)
    {
        return @event switch
        {
            _ => null
        };
    }

    public IInternalCommand? MapToInternalCommand(IDomainEvent @event)
    {
        return @event switch
        {
            _ => null
        };
    }
}
