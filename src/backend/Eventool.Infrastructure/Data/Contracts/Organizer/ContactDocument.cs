using Eventool.Domain.Organizers;
using MongoDB.Bson.Serialization.Attributes;

namespace Eventool.Infrastructure.Data;

public class ContactDocument(Contact contact) : IDocument<Contact>
{
    [BsonId]
    public Guid Id { get; set; } = contact.Id;

    [BsonElement("title")]
    public string Title { get; private set; } = contact.Title;

    [BsonElement("value")]
    public string Value { get; private set; } = contact.Value;

    [BsonElement("description")]
    public string? Description { get; private set; } = contact.Description;

    public Contact ToAggregate() => new(Id, Title, Value, Description);
}