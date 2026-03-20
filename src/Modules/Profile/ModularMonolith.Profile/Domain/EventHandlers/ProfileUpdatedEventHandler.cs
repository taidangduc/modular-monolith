using MediatR;
using ModularMonolith.BuildingBlocks.EventBus;
using ModularMonolith.Profile.Domain.Events;
using ModularMonolith.Profile.Infrastructure;

namespace ModularMonolith.Profile.Domain.EventHandlers;

internal sealed class ProfileUpdatedEventHandler(IEventDispatcher dispatcher, ProfileDbContext dbContext)
    : INotificationHandler<ProfileUpdatedEvent>
{
    public async Task Handle(ProfileUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await dispatcher.DispatchAsync(notification, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
