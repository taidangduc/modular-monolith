using ModularMonolith.BuildingBlocks.Core.SeedWork;
using ModularMonolith.Contracts.Preference.DTOs;

namespace ModularMonolith.Preference.Domain.Events;

public sealed class PreferenceUpdatedEvent(Guid userId, ChannelType channel, bool isOptOut) : DomainEvent
{
    public Guid UserId { get; } = userId;
    public ChannelType Channel { get; } = channel;
    public bool IsOptOut { get; } = isOptOut;
}
