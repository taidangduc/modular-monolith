using Ardalis.GuardClauses;
using BuildingBlocks.Core.CQRS;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.Preference.Domain.Exceptions;
using ModularMonolith.Preference.Infrastructure;

namespace ModularMonolith.Preference.Features.Get;

public record GetPreferenceQuery(Guid Id) : IQuery<PreferenceDto>;

internal class GetPreferenceQueryHandler : IQueryHandler<GetPreferenceQuery, PreferenceDto>
{
    private readonly PreferenceDbContext _preferenceDbContext;

    public GetPreferenceQueryHandler(PreferenceDbContext preferenceDbContext)
    {
        _preferenceDbContext = preferenceDbContext;
    }
    public async Task<PreferenceDto> Handle(GetPreferenceQuery query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var preference = await _preferenceDbContext.Preferences
            .Where(x => x.UserId == query.Id && !x.IsDeleted)
            .GroupBy(x => x.UserId)
            .Select(g => new PreferenceDto(
                g.Key,
                g.Select(p => new ChannelPreference(p.Channel, p.IsOptOut)).ToList()
            ))
            .FirstOrDefaultAsync();

        if (preference is null)
        {
            throw new PreferenceNotFoundException();
        }

        return preference;
    }
}
