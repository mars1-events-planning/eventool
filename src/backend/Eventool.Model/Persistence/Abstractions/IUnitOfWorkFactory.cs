namespace Eventool.Model.Persistence.Abstractions;

public interface IUnitOfWorkFactory<TRepositoryRegistry>
{
    Task<IUnitOfWork<TRepositoryRegistry>> CreateAsync(CancellationToken cancellationToken);
}