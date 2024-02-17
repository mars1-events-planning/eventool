namespace Eventool.Domain.Common;

public class Result<T>
{
    private readonly T _value;

    public Errors Errors { get; }
    public bool IsSuccess => !Errors.Happened;

    public T Value => IsSuccess
        ? _value
        : throw new ResultIsInvalidException(Errors);

    private Result(T value, Errors errors) => (_value, Errors) = (value, errors);

    public static Result<T> Success(T value)
        => new(value, new Errors());

    public static Result<T> Failure(Errors errors)
        => new(default!, errors);

    public static Result<T> Ensure(
        T value,
        params (Func<T, bool> condition, string error)[] conditions)
    {
        var errors = new Errors();
        foreach (var (condition, error) in conditions)
        {
            if (!condition(value))
                errors.AddError(error);
        }

        return errors.Happened
            ? Failure(errors)
            : Success(value);
    }

    public static implicit operator Result<T>(T value)
        => Success(value);

    public static implicit operator Result<T>(Errors errors)
        => Failure(errors);
}

public class ResultIsInvalidException(Errors errors) : Exception("Result is invalid")
{
    public Errors Errors { get; } = errors;
}

public static class ResultExtensions
{
    public static Result<TOut> Map<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> map)
    {
        if (result.IsSuccess)
            return Result<TOut>.Success(map(result.Value));

        return Result<TOut>.Failure(result.Errors);
    }
}