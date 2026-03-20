using System.ComponentModel.DataAnnotations;

namespace ModularMonolith.BuildingBlocks.Core.SeedWork;

public abstract class Entity : HasDomainEvent
{
   public Guid Id { get; set; }
}

public abstract class Entity<TKey> : Entity
{
    public TKey Id { get; set; }
}
