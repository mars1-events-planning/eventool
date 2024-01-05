using MongoDB.Driver;

namespace Eventool.Model.Persistence;

public interface IRepositoryRegistry
{
}

class RepositoryRegistry : IRepositoryRegistry
{
}

public interface IUnitOfWork
{
    public Task<T> RunTransactionAsync<T>(Func<IRepositoryRegistry, Task<T>> transactionAction);
}

public class MongoDbUnitOfWork(IMongoClient client, IRepositoryRegistry repositoryRegistry) : IUnitOfWork
{
    public async Task<T> RunTransactionAsync<T>(Func<IRepositoryRegistry, Task<T>> transactionAction)
    {
        using var session = await client.StartSessionAsync();
        try
        {
            session.StartTransaction();
            var result = await transactionAction(repositoryRegistry);
            await session.CommitTransactionAsync();
            return result;
        }
        catch (Exception)
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }
}