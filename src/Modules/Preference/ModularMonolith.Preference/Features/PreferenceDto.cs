using ModularMonolith.BuildingBlocks.Contracts;

namespace ModularMonolith.Preference.Features;


public record PreferenceDto(Guid UserId, IEnumerable<ChannelPreference> Preferences);

public record ChannelPreference(ChannelType Channel, bool IsOptOut);
