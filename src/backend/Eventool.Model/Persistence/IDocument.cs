using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eventool.Model.Persistence;

public interface IDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    ObjectId Id { get; }

    DateTime CreatedAt => Id.CreationTime;
}

public abstract class Document : IDocument
{
    public ObjectId Id { get; init; }
}