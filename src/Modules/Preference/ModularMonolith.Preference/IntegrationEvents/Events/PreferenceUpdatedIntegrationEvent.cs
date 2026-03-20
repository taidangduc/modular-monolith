using ModularMonolith.BuildingBlocks.EventBus;
using ModularMonolith.Contracts.Preference.DTOs;

namespace ModularMonolith.Preference.IntegrationEvents.Events;

public record PreferenceUpdatedIntegrationEvent(
    Guid UserId,
    ChannelType Channel,
    bool IsOptOut) 
    : IntegrationEvent;