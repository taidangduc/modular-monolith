using BuildingBlocks.Contracts;

namespace User.Features.Preferences;

public record PreferenceDto(Guid UserId, IEnumerable<ChannelPreference> Preferences);

public record ChannelPreference(ChannelType Channel, bool IsOptOut);
