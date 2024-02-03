using System.Linq.Expressions;
using Eventool.Domain.Abstractions;
using MongoDB.Driver;

namespace Eventool.Infrastructure.Data;

public class GenericMongoRepository<TDocument, TAggregate>(IMongoDatabase database)
    where TDocument : IDocument<TAggregate> where TAggregate : IAggregateRoot
{
    protected IMongoCollection<TDocument> Collection { get; } = database.GetCollection<TDocument>(GetCollectionName());

    protected static string GetCollectionName()
        => (typeof(TDocument)
               .GetCustomAttributes(typeof(BsonCollectionAttribute), true)
               .FirstOrDefault() as BsonCollectionAttribute)?.CollectionName
           ?? throw new InvalidOperationException(
               $"{typeof(TDocument).Name} does not have ${nameof(BsonCollectionAttribute)}!");

    protected virtual IQueryable<TDocument> AsQueryable() => Collection.AsQueryable();

    protected virtual IFindFluent<TDocument, TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression) =>
        Collection.Find(filterExpression);

    protected virtual IFindFluent<TDocument, TProjected> FilterBy<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression) =>
        Collection.Find(filterExpression).Project(projectionExpression);

    protected virtual TDocument? FindOne(
        Expression<Func<TDocument, bool>> filterExpression,
        CancellationToken ct) =>
        Collection.Find(filterExpression).FirstOrDefault(ct);

    protected virtual async Task<TDocument?> FindOneAsync(
        Expression<Func<TDocument, bool>> filterExpression,
        CancellationToken ct) =>
        await Collection.Find(filterExpression).FirstOrDefaultAsync(ct);

    protected virtual TDocument? FindById(Guid id, CancellationToken ct) =>
        Collection.Find(FilterById(id)).SingleOrDefault(ct);

    protected virtual async Task<TDocument?> FindByIdAsync(Guid id, CancellationToken ct) =>
        await Collection.Find(FilterById(id)).SingleOrDefaultAsync(ct);

    protected virtual void InsertOne(TDocument document, CancellationToken ct) =>
        Collection.InsertOne(document, cancellationToken: ct);

    protected virtual async Task InsertOneAsync(TDocument document, CancellationToken ct) =>
        await Collection.InsertOneAsync(document, cancellationToken: ct);

    protected void InsertMany(ICollection<TDocument> documents, CancellationToken ct) =>
        Collection.InsertMany(documents, cancellationToken: ct);

    protected virtual async Task InsertManyAsync(ICollection<TDocument> documents, CancellationToken ct) =>
        await Collection.InsertManyAsync(documents, cancellationToken: ct);

    protected void ReplaceOne(TDocument document, CancellationToken ct) =>
        Collection.FindOneAndReplace(FilterById(document.Id), document, cancellationToken: ct);

    protected virtual async Task ReplaceOneAsync(TDocument document, CancellationToken ct) =>
        await Collection.FindOneAndReplaceAsync(FilterById(document.Id), document, cancellationToken: ct);

    protected void DeleteOne(Expression<Func<TDocument, bool>> filterExpression, CancellationToken ct) =>
        Collection.FindOneAndDelete(filterExpression, cancellationToken: ct);

    protected async Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken ct) =>
        await Collection.FindOneAndDeleteAsync(filterExpression, cancellationToken: ct);

    protected void DeleteById(Guid id, CancellationToken ct) =>
        Collection.FindOneAndDelete(FilterById(id), cancellationToken: ct);

    protected async Task DeleteByIdAsync(Guid id, CancellationToken ct) =>
        await Collection.FindOneAndDeleteAsync(FilterById(id), cancellationToken: ct);

    protected void DeleteMany(Expression<Func<TDocument, bool>> filterExpression, CancellationToken ct) =>
        Collection.DeleteMany(filterExpression, cancellationToken: ct);

    protected async Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken ct) =>
        await Collection.DeleteManyAsync(filterExpression, cancellationToken: ct);

    private static FilterDefinition<TDocument> FilterById(Guid id) =>
        Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
}