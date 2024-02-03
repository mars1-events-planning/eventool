using OneOf;
using OneOf.Types;

namespace Eventool.Domain.Organizers;

public interface IOrganizerRepository
{
    Task<OneOf<Organizer>> CreateAsync(Organizer organizer, CancellationToken ct);
    
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken ct);

    Task<OneOf<Organizer, NotFound>> GetByEmail(string email, CancellationToken ct);
}