using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.Notification.IntegrationEvents.Events;

public record PreferenceUpdatedIntegrationEvent(
   Guid UserId,
   string Channel,
   bool IsOptOut
) : IntegrationEvent;
