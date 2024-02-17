using Eventool.Domain.Common;

namespace Eventool.Domain.Organizers;

public record HashedPassword : ValueObject
{
    private HashedPassword(string value, string salt) => (Value, Salt) = (value, salt);

    public static Result<HashedPassword> Create(string hashedPassword, string salt)
        => Result<(string password, string salt)>.Ensure(
                (hashedPassword, salt),
                (
                    condition: x => !string.IsNullOrWhiteSpace(x.password),
                    error: "Хэшированный пароль не может быть пустым!"
                ),
                (
                    condition: x => !string.IsNullOrWhiteSpace(x.salt),
                    error: "Соль не может быть пустой!"
                )
            )
            .Map<(string, string), HashedPassword>(tuple =>
            {
                var (password, salt) = tuple;
                return new HashedPassword(password, salt);
            });

    public string Value { get; }
    public string Salt { get; }
}