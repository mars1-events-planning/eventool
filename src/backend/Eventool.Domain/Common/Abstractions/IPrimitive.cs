namespace Eventool.Domain.Common;

public interface IPrimitive<TPrimitive, TValue>
    where TPrimitive : ValueObject, IPrimitive<TPrimitive, TValue>
{
    TValue Value { get; }

    public static abstract Result<TPrimitive> Create(TValue value);
}