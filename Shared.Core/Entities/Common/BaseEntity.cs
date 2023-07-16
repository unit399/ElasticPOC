using System.ComponentModel.DataAnnotations;

namespace Shared.Core.Entities.Common;

public class BaseEntity
{
    private readonly List<DomainEvent> _domainEvents = new();

    [Key] public int Id { get; set; }

    public bool IsActive { get; set; }

    public ICollection<DomainEvent> GetDomainEvents()
    {
        return _domainEvents;
    }

    protected void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}