using Eventool.Domain.Organizers;
using Marten;

namespace Eventool.Infrastructure.Persistence;

public interface IRepositoryRegistry
{
    IOrganizersRepository OrganizersRepository { get; }
}

public class RepositoryRegistry(IDocumentSession session) : IRepositoryRegistry
{
    public IOrganizersRepository OrganizersRepository { get; } = new OrganizersRepository(session);
}