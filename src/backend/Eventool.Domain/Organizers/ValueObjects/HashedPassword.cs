using Eventool.Domain.Common;

namespace Eventool.Domain.Organizers;

public record HashedPassword : ValueObject
{
    private HashedPassword(string value, string salt) => (Value, Salt) = (value, salt);

    public static Result<HashedPassword> Create(string hashedPassword, string salt)
    {
        var errors = new Errors();
        
        if (string.IsNullOrWhiteSpace(hashedPassword))
            errors.AddError("Хэшированный пароль не может быть пустым!");
        
        if (string.IsNullOrWhiteSpace(salt))
            errors.AddError("Соль не может быть пустой!");
        
        return errors.Any()
            ? errors
            : new HashedPassword(hashedPassword, salt);
    }

    public string Value { get; }
    public string Salt { get; }
}