using ModularMonolith.BuildingBlocks.Core.SeedWork;
using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.Post;

public class PostEventMapper : IEventMapper
{
    public IntegrationEvent? MapToIntegrationEvent(DomainEvent @event)
    {
        switch (@event)
        {
            default:
                return null;
        }
    }
}
