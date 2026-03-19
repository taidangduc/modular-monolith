using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.Notification.IntegrationEvents.Events;

public record UserCreatedIntegrationEvent (
    Guid UserId, 
    string UserName,
    string Name, 
    string Email) 
    : IntegrationEvent;
