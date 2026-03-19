using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.Profile.IntegrationEvents.Events;

public record UserCreatedIntegrationEvent(
    Guid UserId, 
    string UserName,
    string Name, 
    string Email)
    : IntegrationEvent;
