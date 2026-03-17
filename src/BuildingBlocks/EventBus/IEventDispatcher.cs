using ModularMonolith.BuildingBlocks.Core.SeedWork;

namespace ModularMonolith.BuildingBlocks.EventBus;

public interface IEventDispatcher
{
    public Task DispatchAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default);
}
