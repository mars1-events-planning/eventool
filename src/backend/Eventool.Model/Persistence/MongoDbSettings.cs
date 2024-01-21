namespace Eventool.Model.Persistence;

public record MongoDbSettings(string DatabaseName, string ConnectionString)
{
    public const string SectionName = "MongoDbSettings";
}