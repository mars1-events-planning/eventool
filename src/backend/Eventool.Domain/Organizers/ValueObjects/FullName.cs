using Eventool.Domain.Common;

namespace Eventool.Domain.Organizers;

public record FullName : ValueObject, IPrimitive<FullName, string>
{
    private FullName(string value) => Value = value;

    public string Value { get; }

    public static Result<FullName> Create(string value) =>
        Result<string>.Ensure(
                value,
                (
                    condition: x => !string.IsNullOrWhiteSpace(x),
                    error: """Поле "Имя" не может быть пустым!"""
                ),
                (
                    condition: x => x.Length >= 2,
                    error: "Поле \"Имя\" должно содержать минимум 2 символа!"
                ),
                (
                    condition: x => x.Length <= 100,
                    error: "Поле \"Имя\" должно содержать не более 100 символов!"
                )
            )
            .Map<string, FullName>(v => new FullName(v));
}