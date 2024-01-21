using MongoDB.Driver;

namespace Eventool.Model.Persistence.Abstractions;

public interface IRepositoryRegistryFactory<out TRepositoryRegistry>
{
    TRepositoryRegistry Create(IMongoDatabase database);
}