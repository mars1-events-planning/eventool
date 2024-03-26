using Eventool.Domain.Events;
using Marten.Schema;
using Newtonsoft.Json;

namespace Eventool.Infrastructure.Persistence.Events;

public class ChecklistDocument : IDocument<ChecklistDocument, Checklist>
{
    [Identity] [JsonProperty("id")] public Guid Id { get; set; }

    [JsonProperty("title")] public string Title { get; set; } = null!;

    [JsonProperty("description")] public string Description { get; set; } = null!;

    [JsonProperty("checklist_items")] public IEnumerable<ChecklistItemDocument> ChecklistItems { get; set; } = [];

    public static ChecklistDocument Create(Checklist domainObject) => new()
    {
        Id = domainObject.Id,
        Title = domainObject.Title,
        Description = domainObject.Description,
        ChecklistItems = domainObject.ChecklistItems.Select(ChecklistItemDocument.Create)
    };

    public Checklist ToDomainObject()
    {
        var checklist = new Checklist(Id, Title, Description);
        foreach (var item in ChecklistItems.Select(x => x.ToDomainObject()))
            checklist.AddChecklistItem(item);

        return checklist;
    }
}