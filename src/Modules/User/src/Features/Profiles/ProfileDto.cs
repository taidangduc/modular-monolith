using User.Domain.Enums;

namespace User.Features.Profiles;

public record ProfileDto(Guid Id, Guid UserId, string Name, GenderType GenderType, int Age);
