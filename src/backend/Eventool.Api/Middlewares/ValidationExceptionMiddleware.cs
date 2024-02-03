using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;

namespace Eventool.Api.Middlewares;

public class ValidationExceptionMiddleware : IMiddleware
{
    private static async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var errors = new Dictionary<string, string[]>();
        foreach (var failure in exception.Errors)
        {
            if (!errors.TryGetValue(failure.PropertyName, out var messages))
            {
                messages = [];
            }
            errors[failure.PropertyName] = messages.Append(failure.ErrorMessage).ToArray();
        }

        var response = new
        {
            code = context.Response.StatusCode,
            message = "Ошибка валидации",
            details = errors
        };

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });
        await context.Response.WriteAsync(json);
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
    }
}