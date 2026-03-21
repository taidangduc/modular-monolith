using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.Identity.IntegrationEvents.Events;

public record UserCreatedIntegrationEvent(
    Guid UserId,
    string Name,
    string Email)
    : IntegrationEvent;