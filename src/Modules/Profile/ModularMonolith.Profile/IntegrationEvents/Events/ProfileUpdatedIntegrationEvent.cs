using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.Profile.IntegrationEvents.Events;

public record ProfileUpdatedIntegrationEvent(
    Guid UserId,
    string Name,
    string Email
) : IntegrationEvent;
