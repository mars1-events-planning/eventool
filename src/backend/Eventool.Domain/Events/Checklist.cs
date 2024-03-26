using Eventool.Domain.Common;

namespace Eventool.Domain.Events;

public class Checklist(Guid id, string Title, string Description) : Entity<Guid>(id)
{
    private readonly List<ChecklistItem> _checklistItems = [];

    public IReadOnlyList<ChecklistItem> ChecklistItems => _checklistItems.AsReadOnly();

    public void AddChecklistItem(ChecklistItem checklistItem) => _checklistItems.Add(checklistItem);

    public void RemoveChecklistItem(ChecklistItem checklistItem) => _checklistItems.Remove(checklistItem);
}