
using BuildingBlocks.Core.Event;

namespace ModularMonolith.BuildingBlocks.Contracts;

public record UserCreated(Guid UserId, string UserName, string Name, string Email) : IIntegrationEvent;