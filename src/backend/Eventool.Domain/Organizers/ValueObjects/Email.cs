using System.Text.RegularExpressions;
using Eventool.Domain.Common;

namespace Eventool.Domain.Organizers;

public partial record Email : ValueObject, IPrimitive<Email, string>
{
    private Email(string value) => Value = value;
    
    public string Value { get; }
    public static Result<Email> Create(string value)
    {
        var errors = new Errors();

        if (!EmailPattern().IsMatch(value)) 
            errors.AddError("Некорректный email адрес!");

        return errors.Any()
            ? errors
            : new Email(value);
    }

    [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
    private static partial Regex EmailPattern();
}