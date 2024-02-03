using MongoDB.Bson.Serialization.Attributes;

namespace Eventool.Infrastructure.Data;

public interface IDocument<out TAggregate>
{
    [BsonId]
    Guid Id { get; }

    TAggregate ToAggregate();
}