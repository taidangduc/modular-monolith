using Ardalis.GuardClauses;
using BuildingBlocks.Core.CQRS;
using Microsoft.EntityFrameworkCore;
using User.Domain.Exceptions;
using User.Infrastructure;

namespace User.Features.Preferences.Get;

public record GetPreferenceQuery(Guid Id) : IQuery<PreferenceDto>;

internal class GetPreferenceQueryHandler : IQueryHandler<GetPreferenceQuery, PreferenceDto>
{
    private readonly UserDbContext _userDbContext;

    public GetPreferenceQueryHandler(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }
    public async Task<PreferenceDto> Handle(GetPreferenceQuery query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var preference = await _userDbContext.Preferences
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
