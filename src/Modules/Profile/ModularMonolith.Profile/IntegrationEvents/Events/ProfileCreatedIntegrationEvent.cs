using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.Profile.IntegrationEvents.Events;

public record ProfileCreatedIntegrationEvent(
    Guid UserId,
    string Name,
    string Email
) : IntegrationEvent;
