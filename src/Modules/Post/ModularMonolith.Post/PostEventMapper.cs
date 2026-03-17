using BuildingBlocks.Core.Event;
using ModularMonolith.BuildingBlocks.Core.SeedWork;
using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.Post;

public class PostEventMapper : IEventMapper
{
    public IIntegrationEvent? MapToIntegrationEvent(DomainEvent @event)
    {
        switch (@event)
        {
            default:
                return null;
        }
    }
}
