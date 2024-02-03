using Eventool.Domain.Organizers;
using Eventool.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Eventool.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddMongoDb(configuration)
            .AddRepositories();
        
    private static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoDatabase>(_ =>
        {
            var mongoSettings = configuration
                .GetRequiredSection("MongoDB")
                .Get<MongoDbSettings>()!;

            var client = new MongoClient(mongoSettings.ConnectionString);
            return client.GetDatabase(mongoSettings.DatabaseName);
        });
        
#pragma warning disable CS0618
        BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
        BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
#pragma warning restore CS0618

        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
        => services.AddTransient<IOrganizerRepository, MongoOrganizerRepository>();
}