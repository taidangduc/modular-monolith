using MassTransit;
using ModularMonolith.Notification.Infrastructure.Projections;
using ModularMonolith.Notification.IntegrationEvents.Events;

namespace ModularMonolith.Notification.IntegrationEvents.EventHandlers;

public class PreferenceUpdatedEventHandler : IConsumer<PreferenceUpdatedIntegrationEvent>
{
    private readonly ProjectionDispatcher _dispatcher;

    public PreferenceUpdatedEventHandler(ProjectionDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async Task Consume(ConsumeContext<PreferenceUpdatedIntegrationEvent> context)
    {
        if(context.Message is null)
        {
            return;
        }

        await _dispatcher.DispatchAsync(context.Message);
    }
}
