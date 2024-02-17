using Eventool.Infrastructure.Persistence;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weasel.Core;

namespace Eventool.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection serviceCollection,
        IConfiguration configuration) =>
        serviceCollection.AddPersistence(configuration);

    private static IServiceCollection AddPersistence(
        this IServiceCollection serviceCollection,
        IConfiguration configuration) =>
        serviceCollection
            .AddMarten(options =>
            {
                options.Connection(configuration.GetConnectionString("Database")!);
                options.DatabaseSchemaName = "eventool";
                options.UseDefaultSerialization(casing: Casing.SnakeCase, enumStorage: EnumStorage.AsString);
            })
            .OptimizeArtifactWorkflow()
            .Services
            .AddTransient<IUnitOfWork, UnitOfWork>();
}