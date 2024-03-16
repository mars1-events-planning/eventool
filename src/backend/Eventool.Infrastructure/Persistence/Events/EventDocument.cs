using Eventool.Domain.Events;
using Marten.Schema;
using Newtonsoft.Json;

namespace Eventool.Infrastructure.Persistence.Events;

[DocumentAlias("events")]
public class EventDocument : IDocument<EventDocument, Event>
{
    [Identity]
    [JsonProperty("id")]
    public Guid Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = null!;
    
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [JsonProperty("changed_at")]
    public DateTime ChangedAt { get; set; }
    
    [JsonProperty("creator_id")]
    public Guid CreatorId { get; set; }
    
    public Event ToDomainObject() => new(Id, CreatorId, createdAt: CreatedAt, Name)
    {
        ChangedAt = ChangedAt
    };

    public static EventDocument Create(Event domainObject) => new()
    {
        Id = domainObject.Id,
        Name = domainObject.Title,
        CreatorId = domainObject.CreatorId,
        CreatedAt = domainObject.CreatedAt,
        ChangedAt = domainObject.ChangedAt
    };
}