using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.Notification.IntegrationEvents.Events;

public record ProfileUpdatedIntegrationEvent(
    Guid UserId,
    string Name,
    string Email
) : IntegrationEvent;