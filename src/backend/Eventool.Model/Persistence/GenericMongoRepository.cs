using System.Linq.Expressions;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Eventool.Model.Persistence;

public class GenericMongoRepository<TDocument>(IMongoDatabase database) : IMongoRepository<TDocument>
    where TDocument : IDocument
{
    private readonly IMongoCollection<TDocument> _collection =
        database.GetCollection<TDocument>(GetCollectionName());

    private static string GetCollectionName()
        => (typeof(TDocument)
               .GetCustomAttributes(typeof(BsonCollectionAttribute), true)
               .FirstOrDefault() as BsonCollectionAttribute)?.CollectionName
           ?? throw new InvalidOperationException(
               $"{typeof(TDocument).Name} does not have ${nameof(BsonCollectionAttribute)}!");

    public virtual IQueryable<TDocument> AsQueryable() =>
        _collection.AsQueryable();

    public virtual IEnumerable<TDocument> FilterBy(
        Expression<Func<TDocument, bool>> filterExpression) =>
        _collection.Find(filterExpression).ToEnumerable();

    public virtual IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression) =>
        _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();

    public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression) =>
        _collection.Find(filterExpression).FirstOrDefault();

    public virtual async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression) =>
        await _collection.Find(filterExpression).FirstOrDefaultAsync();

    public virtual TDocument FindById(string id) =>
        _collection.Find(FilterById(new ObjectId(id))).SingleOrDefault();

    public virtual async Task<TDocument> FindByIdAsync(string id) =>
        await _collection.Find(FilterById(new ObjectId(id))).SingleOrDefaultAsync();

    public virtual void InsertOne(TDocument document) =>
        _collection.InsertOne(document);

    public virtual async Task InsertOneAsync(TDocument document) =>
        await _collection.InsertOneAsync(document);

    public void InsertMany(ICollection<TDocument> documents) =>
        _collection.InsertMany(documents);

    public virtual async Task InsertManyAsync(ICollection<TDocument> documents) =>
        await _collection.InsertManyAsync(documents);

    public void ReplaceOne(TDocument document) =>
        _collection.FindOneAndReplace(FilterById(document.Id), document);

    public virtual async Task ReplaceOneAsync(TDocument document) =>
        await _collection.FindOneAndReplaceAsync(FilterById(document.Id), document);

    public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression) =>
        _collection.FindOneAndDelete(filterExpression);

    public async Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression) =>
        await _collection.FindOneAndDeleteAsync(filterExpression);

    public void DeleteById(string id) =>
        _collection.FindOneAndDelete(FilterById(new ObjectId(id)));

    public async Task DeleteByIdAsync(string id) =>
        await _collection.FindOneAndDeleteAsync(FilterById(new ObjectId(id)));

    public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression) =>
        _collection.DeleteMany(filterExpression);

    public async Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression) =>
        await _collection.DeleteManyAsync(filterExpression);

    private static FilterDefinition<TDocument> FilterById(ObjectId id) =>
        Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
}