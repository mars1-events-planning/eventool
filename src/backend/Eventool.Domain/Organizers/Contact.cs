using Eventool.Domain.Abstractions;

namespace Eventool.Domain.Organizers;

public class Contact : Entity
{
    public Contact(Guid id, string title, string value, string? description = null)
    {
        Id = id;
        Title = title;
        Value = value;
        Description = description;
    }

    public string? Description { get; private set; }

    public string Value { get; private set; }

    public string Title { get; private set; }
}