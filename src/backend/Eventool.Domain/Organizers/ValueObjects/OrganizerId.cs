using Eventool.Domain.Common;

namespace Eventool.Domain.Organizers;

public record OrganizerId(Guid Value) : ValueObject, IPrimitive<OrganizerId, Guid>
{
    public static Result<OrganizerId> Create(Guid value) => new OrganizerId(value);
}