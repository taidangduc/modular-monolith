namespace ModularMonolith.BuildingBlocks.Core.SeedWork;

public abstract class Entity : HasDomainEvent
{
   public Guid Id { get; set; }
}

public abstract class Entity<TKey> : Entity
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
}
