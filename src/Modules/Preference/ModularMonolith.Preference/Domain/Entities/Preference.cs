using BuildingBlocks.Contracts;
using BuildingBlocks.Core.Model;

namespace ModularMonolith.Preference.Domain.Entities;

public record Preference : Aggregate<Guid>
{
    public Guid UserId { get; private set; }
    public ChannelType Channel { get; private set; }
    public bool IsOptOut { get; private set; }

    public static Preference Create(Guid userId, ChannelType channel, bool isOptOut, bool isDelete = false)
    {
        var preference = new Preference()
        {
            UserId = userId,
            Channel = channel,
            IsOptOut = isOptOut,
            IsDeleted = isDelete,
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
            this.UpdatedAt = DateTime.UtcNow;
        }
    }
}
