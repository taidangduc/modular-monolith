using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.BuildingBlocks.Core.CQRS;
using ModularMonolith.Contracts.Preference.DTOs;
using ModularMonolith.Preference.Domain.Exceptions;
using ModularMonolith.Preference.Infrastructure;

namespace ModularMonolith.Preference.Features.Get;

public record GetPreferenceQuery(Guid Id) : IQuery<PreferenceDTO>;

internal class GetPreferenceQueryHandler : IQueryHandler<GetPreferenceQuery, PreferenceDTO>
{
    private readonly PreferenceDbContext _preferenceDbContext;

    public GetPreferenceQueryHandler(PreferenceDbContext preferenceDbContext)
    {
        _preferenceDbContext = preferenceDbContext;
    }
    public async Task<PreferenceDTO> Handle(GetPreferenceQuery query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var preference = await _preferenceDbContext.Preferences
            .Where(x => x.UserId == query.Id)
            .Select(p => new PreferenceItemDto((ChannelType)p.Channel, p.IsOptOut))
            .ToListAsync();

        if (preference is null)
        {
            throw new PreferenceNotFoundException();
        }

        return new PreferenceDTO(query.Id, preference);
    }
}
