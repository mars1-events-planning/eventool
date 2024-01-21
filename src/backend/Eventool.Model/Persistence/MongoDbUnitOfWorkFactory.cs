using Eventool.Model.Persistence.Abstractions;
using MongoDB.Driver;

namespace Eventool.Model.Persistence;

public class MongoDbUnitOfWorkFactory<TRepositoryRegistry>(
    IMongoClient client,
    IRepositoryRegistryFactory<TRepositoryRegistry> repositoryRegistryFactory,
    MongoDbSettings settings)
    : IUnitOfWorkFactory<TRepositoryRegistry>
{
    public async Task<IUnitOfWork<TRepositoryRegistry>> CreateAsync(CancellationToken cancellationToken)
    {
        var mongoSession = await client.StartSessionAsync(cancellationToken: cancellationToken);
        return new MongoDbUnitOfWork<TRepositoryRegistry>(
            mongoSession,
            repositoryRegistryFactory.Create(mongoSession.Client.GetDatabase(settings.DatabaseName)));
    }
}