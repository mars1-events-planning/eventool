using FluentValidation.Results;
using OneOf;
using OneOf.Types;

namespace Eventool.Application.Utility;

[GenerateOneOf]
public partial class ValidatableResult<TResult> : OneOfBase<Success<TResult>, ValidationResult>
{
}