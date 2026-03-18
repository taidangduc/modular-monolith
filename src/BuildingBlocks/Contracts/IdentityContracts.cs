
using ModularMonolith.BuildingBlocks.EventBus;

namespace ModularMonolith.BuildingBlocks.Contracts;

public record UserCreatedIntegrationEvent(Guid UserId, string UserName, string Name, string Email) : IntegrationEvent;