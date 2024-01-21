using Eventool.Model.Persistence.Abstractions;
using MongoDB.Driver;

namespace Eventool.Model.Persistence;

public sealed class MongoDbUnitOfWork<TRepositoryRegistry>(
    IClientSession session,
    TRepositoryRegistry repositoryRegistry)
    : IUnitOfWork<TRepositoryRegistry>
{
    private bool _disposed;

    public TRepositoryRegistry Repositories { get; } = repositoryRegistry;

    public async Task StartTransactionAsync(CancellationToken _) =>
        await Task.FromResult(() => session.StartTransaction());
    
    public async Task CommitTransactionAsync(CancellationToken cancellationToken) =>
        await session.CommitTransactionAsync(cancellationToken);
    
    public async Task AbortTransactionAsync(CancellationToken cancellationToken) =>
        await session.AbortTransactionAsync(cancellationToken);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing) session?.Dispose();

        _disposed = true;
    }

    ~MongoDbUnitOfWork() => Dispose(false);
}