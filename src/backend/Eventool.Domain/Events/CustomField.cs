namespace Eventool.Domain.Events;

public record CustomField
{
    public string Title { get; set; } = string.Empty;
    
    public string Key { get; set; } = string.Empty;
    
    public string Value { get; set; } = string.Empty;
    
    public string? Description { get; set; }
}