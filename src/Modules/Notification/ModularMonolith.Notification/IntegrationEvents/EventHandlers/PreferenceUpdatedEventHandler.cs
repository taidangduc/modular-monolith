using System.Diagnostics.CodeAnalysis;
using MassTransit;
using ModularMonolith.Notification.Infrastructure;
using ModularMonolith.Notification.Infrastructure.Projections;
using ModularMonolith.Notification.IntegrationEvents.Events;

namespace ModularMonolith.Notification.IntegrationEvents.EventHandlers;

public class PreferenceUpdatedEventHandler : IConsumer<PreferenceUpdatedIntegrationEvent>
{
    private readonly NotificationReadDbContext _readDbContext;

    public PreferenceUpdatedEventHandler(NotificationReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task Consume(ConsumeContext<PreferenceUpdatedIntegrationEvent> context)
    {
        if (context.Message is null)
        {
            return;
        }

        var preference = _readDbContext.PreferenceView
            .FirstOrDefault(p => p.UserId == context.Message.UserId && p.Channel == context.Message.Channel);

        if (preference is not null)
        {
            PreferenceViewProjection.Apply(preference, context.Message);
            await _readDbContext.SaveChangesAsync();
        }
    }
}

[ExcludeFromCodeCoverage]
public class PreferenceUpdatedIntegrationEventConsumerDefinition : ConsumerDefinition<PreferenceUpdatedEventHandler>
{
    public PreferenceUpdatedIntegrationEventConsumerDefinition()
    {
        Endpoint(x => x.Name = "preference-updated");
        ConcurrentMessageLimit = 1;
    }
}
