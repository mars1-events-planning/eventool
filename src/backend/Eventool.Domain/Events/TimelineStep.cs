using Eventool.Domain.Common;

namespace Eventool.Domain.Events;

public class TimelineStep(Guid id, string Title, string Description) : Entity<Guid>(id)
{
    private readonly List<Checklist> _checklists = [];

    public IReadOnlyList<Checklist> Checklists => _checklists.AsReadOnly();
    
    public void AddChecklistItem(Checklist checklist) => _checklists.Add(checklist);

    public void RemoveChecklistItem(Checklist checklist) => _checklists.Remove(checklist);
}