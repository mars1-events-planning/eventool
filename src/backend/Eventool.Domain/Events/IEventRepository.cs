namespace Eventool.Domain.Events;

public interface IEventRepository
{
    public Event Create(Event @event);

    public Task<IEnumerable<Event>> GetListByCreatorIdAsync(Guid creatorId, int pageNumber, CancellationToken ct);

    public Task<Event?> GetByIdAsync(Guid eventId, CancellationToken ct);
}