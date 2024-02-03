using Eventool.Domain.Organizers;
using MongoDB.Driver;
using OneOf;
using OneOf.Types;

namespace Eventool.Infrastructure.Data;

public class MongoOrganizerRepository(IMongoDatabase database)
    : GenericMongoRepository<OrganizerDocument, Organizer>(database), IOrganizerRepository
{
    public async Task<OneOf<Organizer>> CreateAsync(Organizer organizer, CancellationToken ct)
    {
        await InsertOneAsync(new OrganizerDocument(organizer), ct);
        var insertedDocument = await FindByIdAsync(organizer.Id, ct);
        return insertedDocument.ToAggregate();
    }

    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken ct)
    {
        var emailsCount = await FilterBy(x => x.Email == email)
            .CountDocumentsAsync(ct);

        return emailsCount == 0;
    }

    public async Task<OneOf<Organizer, NotFound>> GetByEmail(string email, CancellationToken ct)
    {
        var organizer = await FindOneAsync(x => x.Email == email, ct);
        return organizer is not null
            ? organizer.ToAggregate()
            : new NotFound();;
    }
}