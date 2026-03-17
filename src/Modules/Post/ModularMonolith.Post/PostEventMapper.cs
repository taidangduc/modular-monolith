using BuildingBlocks.Core;
using BuildingBlocks.Core.Event;

namespace ModularMonolith.Post;

public class PostEventMapper : IEventMapper
{
    public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent @event)
    {
        switch (@event)
        {
            default:
                return null;
        }
    }

    public IInternalCommand? MapToInternalCommand(IDomainEvent @event)
    {
        switch (@event)
        {
            default:
                return null;
        }
    }
}
