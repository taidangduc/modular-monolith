using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.BuildingBlocks.Contracts;
using ModularMonolith.BuildingBlocks.Core.CQRS;
using ModularMonolith.Preference.Infrastructure;

namespace ModularMonolith.Preference.Features.Update;

public record UpdatePreferenceCommand(Guid UserId, ChannelType Channel, bool IsOptOut) : ICommand<Guid>;
internal class UpdatePreferenceCommandHandler : ICommandHandler<UpdatePreferenceCommand, Guid>
{
    private readonly PreferenceDbContext _preferenceDbContext;

    public UpdatePreferenceCommandHandler(PreferenceDbContext preferenceDbContext)
    {
        _preferenceDbContext = preferenceDbContext;
    }

    public async Task<Guid> Handle(UpdatePreferenceCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var preference = await _preferenceDbContext.Preferences
            .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Channel == request.Channel, cancellationToken);

        if (preference is null)
        {
            preference = Domain.Entities.Preference.Create(request.UserId, request.Channel, request.IsOptOut);

            await _preferenceDbContext.Preferences.AddAsync(preference, cancellationToken);
        }
        else
        {
            preference.UpdateOptOut(request.IsOptOut);
        }

        await _preferenceDbContext.SaveChangesAsync(cancellationToken);

        return preference.Id;
    }
}
