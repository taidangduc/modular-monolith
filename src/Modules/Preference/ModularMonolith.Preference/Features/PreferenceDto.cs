namespace ModularMonolith.Preference.Features;

public record PreferenceDTO(Guid UserId, IEnumerable<PreferenceOptionDTO> Preferences);

public record PreferenceOptionDTO(ChannelTypeDTO Channel, bool IsOptOut);

public enum ChannelTypeDTO
{
    Email,
    Sms,
    Web
}
