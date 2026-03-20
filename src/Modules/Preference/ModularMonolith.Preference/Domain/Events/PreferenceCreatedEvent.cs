using ModularMonolith.BuildingBlocks.Core.SeedWork;
using ModularMonolith.Contracts.Preference.DTOs;

namespace ModularMonolith.Preference.Domain.Events;

public sealed class PreferenceCreatedEvent(Guid userId, ChannelType channel, bool isOptOut) : DomainEvent
{
    public Guid UserId { get; set; } = userId;
    public ChannelType Channel { get; set; } = channel;
    public bool IsOptOut { get; set; } = isOptOut;
}
