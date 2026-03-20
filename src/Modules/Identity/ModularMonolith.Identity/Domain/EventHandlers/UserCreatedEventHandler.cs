using MediatR;
using ModularMonolith.BuildingBlocks.EventBus;
using ModularMonolith.Identity.Domain.Events;
using ModularMonolith.Identity.Infrastructure;

namespace ModularMonolith.Identity.Domain.EventHandlers;

internal sealed class UserCreatedEventHandler(IEventDispatcher dispatcher, IdentityDbContext dbContext) 
    : INotificationHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        await dispatcher.DispatchAsync(notification, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
