using System.Security.Claims;
using Eventool.Api.GraphQL.Schema.Events.OutputTypes;
using Eventool.Api.GraphQL.Schema.Utility;
using Eventool.Application.UseCases;
using Eventool.Domain.Organizers;
using Eventool.Infrastructure.Utility;
using HotChocolate.Authorization;
using MediatR;
using ValidationException = FluentValidation.ValidationException;

namespace Eventool.Api.GraphQL.Schema;

[MutationType]
[GraphQLName(nameof(RootMutation))]
public class RootMutation
{
    [GraphQLName("registerOrganizer")]
    [Error<ValidationException>]
    public async Task<MutationResult<GqlOrganizer>> RegisterAsync(
        string username,
        string fullName,
        string password,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var organizer = await mediator.Send(
            new RegisterOrganizerRequest(username, fullName, password),
            cancellationToken);

        return new GqlOrganizer(organizer);
    }

    [GraphQLName("login")]
    [Error<UserNotFoundByUsernameException>]
    [Error<WrongPasswordException>]
    [Error<UserNameShouldBeFilledException>]
    public async Task<GqlToken> LoginAsync(
        string username,
        string password,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var token = await mediator.Send(
            new LoginOrganizerRequest(username, password),
            cancellationToken);

        return new GqlToken(token);
    }
    
    [Authorize]
    [GraphQLName("changePassword")]
    [Error<ValidationException>]
    [Error<WrongPasswordException>]
    public async Task<GqlOrganizer> ChangePasswordAsync(
        string oldPassword,
        string newPassword,
        [Service] IMediator mediator,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        var organizerId = claimsPrincipal.Claims
            .Single(x => x.Type == JwtClaims.OrganizerId).Value;

        var guid = Guid.Parse(organizerId);

        var organizer = await mediator.Send(
            new ChangePasswordRequest(oldPassword, newPassword, guid),
            cancellationToken);

        return new GqlOrganizer(organizer);
    }
    
    [Authorize]
    [GraphQLName("editOrganizer")]
    [Error<ValidationException>]
    public async Task<GqlOrganizer> EditOrganizerAsync(
        [Service] IMediator mediator,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken,
        string? username = null,
        string? fullName = null)
    {
        var organizerId = claimsPrincipal.Claims
            .Single(x => x.Type == JwtClaims.OrganizerId).Value;
        var guid = Guid.Parse(organizerId);

        var organizer = await mediator.Send(
            new EditOrganizerRequest(guid, fullName, username),
            cancellationToken);

        return new GqlOrganizer(organizer);
    }
    
    [Authorize]
    [GraphQLName("createEvent")]
    [Error<ValidationException>]
    public async Task<GqlEvent> CreateEventAsync(
        string title,
        [Service] IMediator mediator,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        var organizerId = claimsPrincipal.Claims
            .Single(x => x.Type == JwtClaims.OrganizerId).Value;
        var guid = Guid.Parse(organizerId);

        var @event = await mediator.Send(
            new CreateEventCommand(title, guid),
            cancellationToken);

        return new GqlEvent(@event);
    }
}