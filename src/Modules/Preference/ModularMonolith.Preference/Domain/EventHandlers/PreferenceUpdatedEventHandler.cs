using MediatR;
using ModularMonolith.BuildingBlocks.EventBus;
using ModularMonolith.Preference.Domain.Events;
using ModularMonolith.Preference.Infrastructure;

namespace ModularMonolith.Preference.Domain.EventHandlers;

internal sealed class PreferenceUpdatedEventHandler(IEventDispatcher dispatcher, PreferenceDbContext dbContext) 
    : INotificationHandler<PreferenceUpdatedEvent>
{
    public async Task Handle(PreferenceUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await dispatcher.DispatchAsync(notification, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
