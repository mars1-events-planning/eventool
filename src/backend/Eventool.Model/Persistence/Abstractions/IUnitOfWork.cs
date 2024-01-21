namespace Eventool.Model.Persistence.Abstractions;

public interface IUnitOfWork<out TRepositoryRegistry> : IDisposable
{
    TRepositoryRegistry Repositories { get; }

    Task StartTransactionAsync(CancellationToken cancellationToken);

    Task CommitTransactionAsync(CancellationToken cancellationToken);

    Task AbortTransactionAsync(CancellationToken cancellationToken);
}