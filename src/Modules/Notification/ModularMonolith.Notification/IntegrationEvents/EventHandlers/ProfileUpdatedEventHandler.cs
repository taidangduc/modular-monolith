using MassTransit;
using ModularMonolith.Notification.Infrastructure.Projections;
using ModularMonolith.Notification.IntegrationEvents.Events;

namespace ModularMonolith.Notification.IntegrationEvents.EventHandlers;

public class ProfileUpdatedEventHandler : IConsumer<ProfileUpdatedIntegrationEvent>
{
    private readonly ProjectionDispatcher _dispatcher;

    public ProfileUpdatedEventHandler(ProjectionDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async Task Consume(ConsumeContext<ProfileUpdatedIntegrationEvent> context)
    {
        if(context.Message is null)
        {
            return;
        }

        await _dispatcher.DispatchAsync(context.Message);
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