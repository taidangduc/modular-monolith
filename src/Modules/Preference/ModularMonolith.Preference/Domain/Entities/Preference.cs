using ModularMonolith.BuildingBlocks.Core.SeedWork;
using ModularMonolith.Contracts.Preference.DTOs;
using ModularMonolith.Preference.Domain.Events;

namespace ModularMonolith.Preference.Domain.Entities;

public class Preference : Entity, IAggregate
{
    public Guid UserId { get; private set; }
    public ChannelType Channel { get; private set; }
    public bool IsOptOut { get; private set; }

    public static Preference Create(Guid userId, ChannelType channel, bool isOptOut)
    {
        var preference = new Preference()
        {
            UserId = userId,
            Channel = channel,
            IsOptOut = isOptOut,
        };

        preference.AddDomainEvent(new PreferenceCreatedEvent(userId, channel, isOptOut));

        return preference;
    }

    public static IReadOnlyList<Preference> CreateForUser(Guid userId)
    {
        return Enum.GetValues<ChannelType>()
            .Select(channel => Create(userId, channel, GetOptOutValueRule(channel)))
            .ToList();
    }

    private static bool GetOptOutValueRule(ChannelType channel)
    {
        return channel switch
        {
            ChannelType.Email => false,
            ChannelType.Sms => true,
            ChannelType.Web => false,
            _ => false,
        };
    }

    public void UpdateOptOut(ChannelType channel, bool isOptOut)
    {
        if (this.IsOptOut != isOptOut)
        {
            this.IsOptOut = isOptOut;
        }

        AddDomainEvent(new PreferenceUpdatedEvent(this.UserId, channel, isOptOut));
    }
}
