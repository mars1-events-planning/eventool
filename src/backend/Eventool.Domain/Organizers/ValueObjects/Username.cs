using System.Text.RegularExpressions;
using Eventool.Domain.Common;

namespace Eventool.Domain.Organizers;

public partial record Username : ValueObject, IPrimitive<Username, string>
{
    private Username(string value) => Value = value;
    
    public string Value { get; }

    public static Result<Username> Create(string value) =>
        Result<string>.Ensure(
                value,
                (
                    condition: x => !string.IsNullOrWhiteSpace(x),
                    error: """Поле "Имя пользователя" не может быть пустым!"""
                ),
                (
                    condition: x => x.Length >= 2,
                    error: "Поле \"Имя пользователя\" должно содержать минимум 2 символа!"
                ),
                (
                    condition: x => x.Length <= 100,
                    error: "Поле \"Имя пользователя\" должно содержать не более 100 символов!"
                ),
                (
                    condition: x => UrlFriendly().IsMatch(x),
                    error: "Поле \"Имя пользователя\" должно содержать только латинские буквы, цифры, символы \"_\" и \"-\"!"
                )
            )
            .Map<string, Username>(v => new Username(v));

    [GeneratedRegex("^[a-zA-Z0-9_-]*$")]
    private static partial Regex UrlFriendly();
}