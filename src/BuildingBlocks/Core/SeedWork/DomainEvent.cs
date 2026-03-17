using MediatR;

namespace ModularMonolith.BuildingBlocks.Core.SeedWork;

public abstract class DomainEvent : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
