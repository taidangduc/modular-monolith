using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.Notification.IntegrationEvents.Events;

public record ProfileCreatedIntegrationEvent(
    Guid UserId,
    string FullName,
    string Email
) : IntegrationEvent;