using BuildingBlocks.Core.Event;
using ModularMonolith.BuildingBlocks.Core.SeedWork;

namespace ModularMonolith.BuildingBlocks.EventBus;

public interface IEventMapper
{
    IIntegrationEvent? MapToIntegrationEvent(DomainEvent @event);
}
