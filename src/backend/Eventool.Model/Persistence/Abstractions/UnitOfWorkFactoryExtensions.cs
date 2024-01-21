namespace Eventool.Model.Persistence.Abstractions;

public static class UnitOfWorkFactoryExtensions
{
    public static async Task<TResult> ExecuteReadOnlyAsync<TRepositoryRegistry, TResult>(
        this IUnitOfWorkFactory<TRepositoryRegistry> unitOfWorkFactory,
        Func<TRepositoryRegistry, CancellationToken, Task<TResult>> action,
        CancellationToken cancellationToken)
    {
        using var uow = await unitOfWorkFactory.CreateAsync(cancellationToken);
        return await action(uow.Repositories, cancellationToken);
    }

    public static async Task<TResult> ExecuteTransactionAsync<TRepositoryRegistry, TResult>(
        this IUnitOfWorkFactory<TRepositoryRegistry> unitOfWorkFactory,
        Func<TRepositoryRegistry, CancellationToken, Task<TResult>> action,
        CancellationToken cancellationToken)
    {
        using var uow = await unitOfWorkFactory.CreateAsync(cancellationToken);
        try
        {
            await uow.StartTransactionAsync(cancellationToken);
            var result = await action(uow.Repositories, cancellationToken);
            await uow.CommitTransactionAsync(cancellationToken);
            return result;
        }
        catch (Exception)
        {
            await uow.AbortTransactionAsync(cancellationToken);
            throw;
        }
    }
}