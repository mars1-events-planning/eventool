using System.Security.Claims;
using Eventool.Api.Apis.Services;
using Eventool.Api.Utility;
using Eventool.Application.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Eventool.Api.Apis;

public static class OrganizerApi
{
    public static IEndpointRouteBuilder MapOrganizerApi(this IEndpointRouteBuilder routeBuilder)
    {
        var auth = routeBuilder.MapGroup("/auth");

        auth.MapPost("/sign-up", SignUpAsync);
        auth.MapPost("/sign-in", SignInAsync);

        return routeBuilder;
    }

    public static async Task<Results<Ok<SignUpResult>, ValidationProblem>> SignUpAsync(
        [AsParameters] OrganizerApiServices services,
        [FromBody] OrganizerSignUpCommand command,
        CancellationToken ct)
    {
        var signUpResult = await services.Mediator.Send(command, ct);
        return signUpResult.Match<Results<Ok<SignUpResult>, ValidationProblem>>(
            organizerId => TypedResults.Ok(new SignUpResult(organizerId.Value)),
            validationResult => validationResult.ValidationProblem());
    }
    
    public static async Task<Results<SignInHttpResult, ValidationProblem>> SignInAsync(
        [AsParameters] OrganizerApiServices services,
        [FromBody] CheckCredentialsCommand command,
        CancellationToken ct)
    {
        var checkCredentialsResult = await services.Mediator.Send(command, ct);
        return checkCredentialsResult.Match<Results<SignInHttpResult, ValidationProblem>>(
            organizer => TypedResults.SignIn(new ClaimsPrincipal()),
            wrongCredentials => TypedResults.ValidationProblem(
                errors: new Dictionary<string, string[]>(),
                title: WrongCredentials.Message));
    }
}

public record SignUpResult(Guid OrganizerId);