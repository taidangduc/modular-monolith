using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.Notification.IntegrationEvents.Events;

public record ProfileCreatedIntegrationEvent(
    Guid UserId,
    string Name,
    string Email
) : IntegrationEvent;