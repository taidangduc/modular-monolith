using ModularMonolith.BuildingBlocks.Core.SeedWork;

namespace ModularMonolith.Profile.Domain.Events;

public class ProfileCreatedEvent(Guid UserId, string Name, string Email) : DomainEvent
{
    public Guid UserId { get; } = UserId;
    public string Name { get; } = Name;
    public string Email { get; } = Email;
}