using BuildingBlocks.Core.Event;
using ModularMonolith.BuildingBlocks.Core.SeedWork;
using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.Notification;
public sealed class NotificationEventMapper : IEventMapper
{
    public IIntegrationEvent? MapToIntegrationEvent(DomainEvent @event)
    {
        return @event switch
        {
            _ => null
        };
    }
}
