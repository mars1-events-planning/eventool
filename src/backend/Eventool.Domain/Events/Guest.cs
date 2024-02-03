using Eventool.Domain.Abstractions;

namespace Eventool.Domain.Events;

public class Guest : Entity
{
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public IEnumerable<CustomField> AdditionalData { get; set; } = [];
    
    public IEnumerable<TimelineStage> PresentAt { get; set; } = [];
}