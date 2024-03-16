using Eventool.Domain.Events;
using Eventool.Infrastructure.Persistence;

namespace Eventool.Api.GraphQL.Schema.Events.OutputTypes;

public class GqlEvent(Event @event)
{
    public Guid Id { get; } = @event.Id;
    
    public string Title { get; } = @event.Title;
    
    public DateTime CreatedAt { get; } = @event.CreatedAt;
    
    public DateTime ChangedAt { get; } = @event.ChangedAt;

    [GraphQLName("creator")]
    public async Task<GqlOrganizer> GetCreator(
        [Service] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken) => await unitOfWork.ExecuteReadOnlyAsync(
        action: async repositories =>
        {
            var organizer = await repositories.OrganizersRepository.TryGetByIdAsync(@event.CreatorId, cancellationToken);
            return organizer is null ? throw new NotFoundByUsernameException() : new GqlOrganizer(organizer);
        }, cancellationToken);
}