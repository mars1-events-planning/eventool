using System.Text.RegularExpressions;
using Eventool.Domain.Common;

namespace Eventool.Domain.Organizers;

public partial record Username : ValueObject, IPrimitive<Username, string>
{
    private Username(string value) => Value = value;
    
    public string Value { get; }
    public static Result<Username> Create(string value)
    {
        var errors = new Errors();
        
        if (string.IsNullOrWhiteSpace(value))
            errors.AddError("""Поле "Имя пользователя" не может быть пустым!""");
        
        if (value.Length < 2)
            errors.AddError("Поле \"Имя пользователя\" должно содержать минимум 2 символа!");
        
        if (value.Length > 100)
            errors.AddError("Поле \"Имя пользователя\" должно содержать не более 100 символов!");
        
        if (!UrlFriendly().IsMatch(value))
            errors.AddError("Поле \"Имя пользователя\" должно содержать только латинские буквы, цифры, символы \"_\" и \"-\"!");

        return errors.Any()
            ? errors
            : new Username(value);
    }

    [GeneratedRegex("^[a-zA-Z0-9_-]*$")]
    private static partial Regex UrlFriendly();
}