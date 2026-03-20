using System.Diagnostics.CodeAnalysis;
using MassTransit;
using ModularMonolith.Notification.Infrastructure;
using ModularMonolith.Notification.Infrastructure.Projections;
using ModularMonolith.Notification.IntegrationEvents.Events;

namespace ModularMonolith.Notification.IntegrationEvents.EventHandlers;

public class PreferenceCreatedEventHandler : IConsumer<PreferenceCreatedIntegrationEvent>
{
    private readonly NotificationReadDbContext _readDbContext;

    public PreferenceCreatedEventHandler(NotificationReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task Consume(ConsumeContext<PreferenceCreatedIntegrationEvent> context)
    {
        if (context.Message is null)
        {
            return;
        }

        var preference = _readDbContext.preferenceView
            .FirstOrDefault(p => p.UserId == context.Message.UserId && p.Channel == context.Message.Channel);

        if (preference is null)
        {
            var newPreference = PreferenceViewProjection.Create(new PreferenceView(), context.Message);

            _readDbContext.preferenceView.Add(newPreference);
            await _readDbContext.SaveChangesAsync();
        }
    }
}

[ExcludeFromCodeCoverage]
public class PreferenceCreatedIntegrationEventConsumerDefinition : ConsumerDefinition<PreferenceCreatedEventHandler>
{
    public PreferenceCreatedIntegrationEventConsumerDefinition()
    {
        Endpoint(x => x.Name = "preference-created");
        ConcurrentMessageLimit = 1;
    }
}
