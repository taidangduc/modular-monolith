
using BuildingBlocks.Core.Event;

namespace BuildingBlocks.Contracts;

public record UserCreated(Guid UserId, string UserName, string Name, string Email) : IIntegrationEvent;

