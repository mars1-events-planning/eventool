using Eventool.Domain.Abstractions;

namespace Eventool.Domain.Events;

public class Event : Entity, IAggregateRoot
{
    public string Title { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public Uri? ImageUrl { get; set; }
    
    public IEnumerable<TimelineStage> Timeline { get; set; } = [];

    public IEnumerable<Guest> Guests { get; set; } = [];
    
    public IEnumerable<Checklist> Checklists { get; set; } = [];
}