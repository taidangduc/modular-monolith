using System.Diagnostics.CodeAnalysis;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModularMonolith.Preference.Infrastructure;
using ModularMonolith.Preference.IntegrationEvents.Events;

namespace ModularMonolith.Preference.IntegrationEvents.EventHandlers;

public class UserCreatedEventHandler : IConsumer<UserCreatedIntegrationEvent>
{
    private readonly PreferenceDbContext _preferenceDbContext;
    private readonly ILogger<UserCreatedEventHandler> _logger;

    public UserCreatedEventHandler(PreferenceDbContext preferenceDbContext, ILogger<UserCreatedEventHandler> logger)
    {
        _preferenceDbContext = preferenceDbContext;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
    {
        _logger.LogInformation($"Consumer for {nameof(UserCreatedIntegrationEvent)} started");

        var data = context.Message;

        if (data.Equals(default))
        {
            return;
        }

        var preference = await _preferenceDbContext.Preferences
            .Where(x => x.UserId == data.UserId)
            .ToListAsync();

        if (preference != null)
        {
            return;
        }

        var preferences = Domain.Entities.Preference.CreateForUser(data.UserId);

        await _preferenceDbContext.Preferences.AddRangeAsync(preferences);

        await _preferenceDbContext.SaveChangesAsync();
    }
}

[ExcludeFromCodeCoverage]
public class UserCreatedIntegrationEventConsumerDefinition : ConsumerDefinition<UserCreatedEventHandler>
{
    public UserCreatedIntegrationEventConsumerDefinition()
    { 
        Endpoint(x => x.Name = "user-created");
        ConcurrentMessageLimit = 1;
    }
}
