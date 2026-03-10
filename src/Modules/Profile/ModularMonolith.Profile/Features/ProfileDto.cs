using ModularMonolith.Profile.Domain.Enums;

namespace ModularMonolith.Profile.Features;

public record ProfileDto(Guid Id, Guid UserId, string Name, GenderType GenderType, int Age);
