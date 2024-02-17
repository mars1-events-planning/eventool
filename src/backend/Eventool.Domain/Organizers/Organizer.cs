using Eventool.Domain.Common;

namespace Eventool.Domain.Organizers;

public class Organizer : Entity<OrganizerId>, IAggregateRoot
{
    public FullName FullName { get; }

    public Credentials Credentials { get; }

    private Organizer(
        OrganizerId id,
        FullName fullName,
        Credentials credentials) : base(id)
    {
        FullName = fullName;
        Credentials = credentials;
    }

    public static Result<Organizer> Create(
        OrganizerId id,
        FullName fullName,
        Credentials credentials) => new Organizer(id, fullName, credentials);
}