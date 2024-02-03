using Eventool.Domain.Abstractions;

namespace Eventool.Domain.Events;

public class TimelineStage : Entity
{
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public DateTime StartDateTimeUtc { get; set; }
    
    public DateTime EndDateTimeUtc { get; set; }
    
    public string Location { get; set; } = string.Empty;
}