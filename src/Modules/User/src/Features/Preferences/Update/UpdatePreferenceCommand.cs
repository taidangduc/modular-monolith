using Ardalis.GuardClauses;
using BuildingBlocks.Core.CQRS;
using Microsoft.EntityFrameworkCore;
using User.Domain.Entities;
using User.Infrastructure;
using BuildingBlocks.Contracts;

namespace User.Features.Preferences.Update;

public record UpdatePreferenceCommand(Guid UserId, ChannelType Channel, bool IsOptOut) : ICommand<Guid>;
internal class UpdatePreferenceCommandHandler : ICommandHandler<UpdatePreferenceCommand, Guid>
{
    private readonly UserDbContext _userDbContext;

    public UpdatePreferenceCommandHandler(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public async Task<Guid> Handle(UpdatePreferenceCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var preference = await _userDbContext.Preferences
            .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Channel == request.Channel, cancellationToken);

        if (preference is null)
        {
            preference = Preference.Create(request.UserId, request.Channel, request.IsOptOut);

            await _userDbContext.Preferences.AddAsync(preference, cancellationToken);
        }
        else
        {
            preference.UpdateOptOut(request.IsOptOut);
        }

        await _userDbContext.SaveChangesAsync(cancellationToken);

        return preference.Id;
    }
}