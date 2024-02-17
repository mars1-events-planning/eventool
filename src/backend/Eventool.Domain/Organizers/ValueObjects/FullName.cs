using Eventool.Domain.Common;

namespace Eventool.Domain.Organizers;

public record FullName : ValueObject, IPrimitive<FullName, string>
{
    private FullName(string value) => Value = value;
    
    public string Value { get; }
    public static Result<FullName> Create(string value)
    {
        var errors = new Errors();
        
        if (string.IsNullOrWhiteSpace(value))
            errors.AddError("""Поле "Имя" не может быть пустым!""");
        
        if (value.Length < 2)
            errors.AddError("Поле \"Имя\" должно содержать минимум 2 символа!");
        
        if (value.Length > 100)
            errors.AddError("Поле \"Имя\" должно содержать не более 100 символов!");

        return errors.Any()
            ? errors
            : new FullName(value);
    }
}