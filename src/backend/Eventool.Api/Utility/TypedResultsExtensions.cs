using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Eventool.Api.Utility;

public static class TypedResultsExtensions
{
    public static ValidationProblem ValidationProblem(this ValidationResult validationResult)
        => TypedResults.ValidationProblem(
            errors: validationResult.ToDictionary(),
            title: "Ошибка валидации");
}