using Eventool.Domain.Common;

namespace Eventool.Domain.Organizers;

public record Credentials(Username Username, HashedPassword Password) : ValueObject;