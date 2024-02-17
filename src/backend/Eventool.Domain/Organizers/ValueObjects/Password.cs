using System.Text.RegularExpressions;
using Eventool.Domain.Common;

namespace Eventool.Domain.Organizers;

public partial record Password : ValueObject, IPrimitive<Password, string>
{
    private Password(string value) => Value = value;

    public string Value { get; }

    public static Result<Password> Create(string value) =>
        Result<string>.Ensure(
                value,
                (
                    condition: x => PasswordPattern().IsMatch(x),
                    error: "Пароль должен содержать минимум 8 символов, как минимум одну заглавную букву, одну строчную букву и одну цифру!"
                )
            )
            .Map<string, Password>(v => new Password(v));

    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")]
    private static partial Regex PasswordPattern();
}