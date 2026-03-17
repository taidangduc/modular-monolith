using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModularMonolith.BuildingBlocks.Contracts;
using ModularMonolith.Preference.Infrastructure;

namespace ModularMonolith.Preference.IntegrationEvents.EventHandlers;

public class UserCreatedEventHandler : IConsumer<UserCreated>
{
    private readonly PreferenceDbContext _preferenceDbContext;
    private readonly ILogger<UserCreatedEventHandler> _logger;

    public UserCreatedEventHandler(PreferenceDbContext preferenceDbContext, ILogger<UserCreatedEventHandler> logger)
    {
        _preferenceDbContext = preferenceDbContext;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        _logger.LogInformation($"Consumer for {nameof(UserCreated)} started");

        var data = context.Message;

        if (data.Equals(default))
        {
            return;
        }

        var preference = await _preferenceDbContext.Preferences.FirstOrDefaultAsync(x => x.UserId == data.UserId);

        if (preference != null)
        {
            return;
        }

        var preferences = Domain.Entities.Preference.SetDefaultValues(data.UserId);

        await _preferenceDbContext.Preferences.AddRangeAsync(preferences);

        await _preferenceDbContext.SaveChangesAsync();
    }
}
