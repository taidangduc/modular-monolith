using ModularMonolith.BuildingBlocks.Core.SeedWork;

namespace ModularMonolith.BuildingBlocks.EventBus;

public interface IEventMapper
{
    IntegrationEvent? MapToIntegrationEvent(DomainEvent @event);
}
