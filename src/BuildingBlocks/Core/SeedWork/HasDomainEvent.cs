using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ModularMonolith.BuildingBlocks.Core.SeedWork;

public class HasDomainEvent : IHasDomainEvent
{
    public List<DomainEvent> _domainEvents = [];

    [NotMapped]
    [JsonIgnore]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
