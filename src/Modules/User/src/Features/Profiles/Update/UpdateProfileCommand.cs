using Ardalis.GuardClauses;
using BuildingBlocks.Core.CQRS;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

using User.Domain.Enums;
using User.Infrastructure;

namespace User.Features.Profiles.Update;

public record UpdateProfileCommand(Guid UserId, GenderType GenderType, int Age) : ICommand<Guid>;

internal class UpdateProfileHandler : ICommandHandler<UpdateProfileCommand, Guid>
{
    private readonly UserDbContext _userDbContext;

    public UpdateProfileHandler(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public async Task<Guid> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var profile = await _userDbContext.Profiles
            .SingleOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

        if (profile is null)
        {
            return default;
        }

        profile.Update(profile.UserId, profile.UserName, profile.Name, profile.Email, request.GenderType, request.Age);

        await _userDbContext.SaveChangesAsync(cancellationToken);

        return profile.Id;
    }
}
