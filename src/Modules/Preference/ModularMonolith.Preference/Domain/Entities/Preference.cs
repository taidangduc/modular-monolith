using ModularMonolith.BuildingBlocks.Core.SeedWork;
using ModularMonolith.Preference.Domain.Enums;

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

        return preference;
    }

    public static IReadOnlyList<Preference> SetDefaultValues(Guid userId)
    {
        return Enum.GetValues<ChannelType>()
            .Select(channel => Create(userId, channel, false))
            .ToList();
    }

    public void UpdateOptOut(bool isOptOut)
    {
        if (this.IsOptOut != isOptOut)
        {
            this.IsOptOut = isOptOut;
        }
    }
}
