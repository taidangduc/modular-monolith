using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModularMonolith.BuildingBlocks.Contracts;
using ModularMonolith.Profile.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace ModularMonolith.Profile.IntegrationEvents.EventHandlers;

public class UserCreatedEventHandler : IConsumer<UserCreatedIntegrationEvent>
{
    private readonly ProfileDbContext _profileDbContext;
    private readonly ILogger<UserCreatedEventHandler> _logger;

    public UserCreatedEventHandler(ProfileDbContext profileDbContext, ILogger<UserCreatedEventHandler> logger)
    {
        _profileDbContext = profileDbContext;
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

        var profileEntity = await _profileDbContext.Profiles.FirstOrDefaultAsync(x => x.UserId == data.UserId);

        if (profileEntity != null)
        {
            return;
        }

        var profile = Domain.Entities.Profile.Create(data.UserId, data.UserName, data.Name, data.Email);

        await _profileDbContext.Profiles.AddAsync(profile);

        await _profileDbContext.SaveChangesAsync();
    }
}

[ExcludeFromCodeCoverage]
internal sealed class UserCreatedEventHandlerDefinition : ConsumerDefinition<UserCreatedEventHandler>
{
    public UserCreatedEventHandlerDefinition()
    {
       Endpoint(x => x.Name = "user-created");
       ConcurrentMessageLimit = 1;
    }
}
