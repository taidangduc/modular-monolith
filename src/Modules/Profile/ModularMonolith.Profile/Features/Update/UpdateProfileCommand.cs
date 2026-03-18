using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.BuildingBlocks.Core.CQRS;
using ModularMonolith.Profile.Domain.Enums;
using ModularMonolith.Profile.Infrastructure;

namespace ModularMonolith.Profile.Features.Update;

public record UpdateProfileCommand(Guid UserId, GenderType GenderType, int Age) : ICommand<Guid>;

internal class UpdateProfileHandler : ICommandHandler<UpdateProfileCommand, Guid>
{
    private readonly ProfileDbContext _profileDbContext;

    public UpdateProfileHandler(ProfileDbContext profileDbContext)
    {
        _profileDbContext = profileDbContext;
    }

    public async Task<Guid> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var profile = await _profileDbContext.Profiles.SingleOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

        if (profile is null)
        {
            return default;
        }

        profile.Update(profile.UserId, profile.UserName, profile.Name, profile.Email, request.GenderType, request.Age);

        await _profileDbContext.SaveChangesAsync(cancellationToken);

        return profile.Id;
    }
}
