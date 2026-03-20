using MediatR;
using ModularMonolith.BuildingBlocks.EventBus;
using ModularMonolith.Profile.Domain.Events;
using ModularMonolith.Profile.Infrastructure;

namespace ModularMonolith.Profile.Domain.EventHandlers;

internal sealed class ProfileCreatedEventHandler(IEventDispatcher dispatcher, ProfileDbContext dbContext)
    : INotificationHandler<ProfileCreatedEvent>
{
    public async Task Handle(ProfileCreatedEvent notification, CancellationToken cancellationToken)
    {
        await dispatcher.DispatchAsync(notification, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
