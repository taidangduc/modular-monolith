namespace ModularMonolith.Contracts.Preference.DTOs;

public record PreferenceDTO(Guid UserId, IEnumerable<PreferenceItemDto> Preferences);

public record PreferenceItemDto(ChannelType Channel, bool IsOptOut);

public enum ChannelType
{
    Email,
    Sms,
    Web
}