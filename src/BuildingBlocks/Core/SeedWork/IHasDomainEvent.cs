namespace ModularMonolith.BuildingBlocks.Core.SeedWork;

public interface IHasDomainEvent
{
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }
}
