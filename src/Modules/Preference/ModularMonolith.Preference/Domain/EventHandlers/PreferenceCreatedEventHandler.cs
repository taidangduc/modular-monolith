using MediatR;
using ModularMonolith.BuildingBlocks.EventBus;
using ModularMonolith.Preference.Domain.Events;
using ModularMonolith.Preference.Infrastructure;

namespace ModularMonolith.Preference.Domain.EventHandlers;

internal sealed class PreferenceCreatedEventHandler(IEventDispatcher dispatcher, PreferenceDbContext dbContext) 
    : INotificationHandler<PreferenceCreatedEvent>
{
    public async Task Handle(PreferenceCreatedEvent notification, CancellationToken cancellationToken)
    {
        await dispatcher.DispatchAsync(notification, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
