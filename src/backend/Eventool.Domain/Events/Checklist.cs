using Eventool.Domain.Abstractions;

namespace Eventool.Domain.Events;

public class Checklist : Entity
{
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public IEnumerable<ChecklistItem> Items { get; set; } = [];
}

public record ChecklistItem
{
    public string Title { get; set; }
    
    public int Index { get; set; } 
 
    public string? Description { get; set; }

    public bool IsDone { get; set; } = false;
}