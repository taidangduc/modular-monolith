using ModularMonolith.BuildingBlocks.Core.SeedWork;

namespace ModularMonolith.Identity.Domain.Events;

public sealed class UserCreatedEvent(Guid UserId, string Name, string Email) : DomainEvent
{
    public Guid UserId { get; } = UserId;
    public string Name { get; } = Name;
    public string Email { get; } = Email;
}
