using Eventool.Domain.Organizers;
using Marten;

namespace Eventool.Infrastructure.Persistence;

public class OrganizersRepository(IDocumentSession session) :
    Repository<OrganizerId, Organizer, OrganizerDocument>(session),
    IOrganizersRepository
{
    public void Create(Organizer organizer)
    {
        Session.Store(organizer);
    }
}