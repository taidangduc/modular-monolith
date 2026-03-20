using MassTransit;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.Notification.Infrastructure;
using ModularMonolith.Notification.Infrastructure.Projections;
using ModularMonolith.Notification.IntegrationEvents.Events;

namespace ModularMonolith.Notification.IntegrationEvents.EventHandlers;

public class ProfileUpdatedEventHandler : IConsumer<ProfileUpdatedIntegrationEvent>
{
    private readonly NotificationReadDbContext _readDbContext;

    public ProfileUpdatedEventHandler(NotificationReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task Consume(ConsumeContext<ProfileUpdatedIntegrationEvent> context)
    {
        if (context.Message is null)
        {
            return;
        }

        var profile = await _readDbContext.profileView
            .FirstOrDefaultAsync(p => p.UserId == context.Message.UserId);

        if (profile is not null)
        {
            ProfileViewProjection.Apply(profile, context.Message);
            await _readDbContext.SaveChangesAsync();
        }
    }
}

public class ProfileUpdatedIntegrationEventConsumerDefinition : ConsumerDefinition<ProfileUpdatedEventHandler>
{
    public ProfileUpdatedIntegrationEventConsumerDefinition()
    {
        Endpoint(x => x.Name = "profile-updated");
        ConcurrentMessageLimit = 1;
    }
}