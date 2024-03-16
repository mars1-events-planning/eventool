using Eventool.Domain.Common;

namespace Eventool.Domain.Events;

public class Event(Guid id, Guid creatorId, DateTime createdAt, string title) : Entity<Guid>(id), IAggregateRoot
{
    private DateTime _changedAt = createdAt;
    
    public Guid CreatorId { get; } = creatorId;

    public DateTime CreatedAt { get; } = createdAt;

    public DateTime ChangedAt { get => _changedAt; init => _changedAt = value; }

    public string Title { get; } = title;

    private void Changed() => _changedAt = DateTime.UtcNow;
}