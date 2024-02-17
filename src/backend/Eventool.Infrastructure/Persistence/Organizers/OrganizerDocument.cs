using Eventool.Domain.Organizers;
using Marten.Schema;
using Newtonsoft.Json;

namespace Eventool.Infrastructure.Persistence;

[DocumentAlias("organizers")]
public class OrganizerDocument : IDocument<OrganizerDocument, Organizer>
{
    [Identity]
    [JsonProperty("id")]
    public Guid Id { get; set; }
    
    [JsonProperty("full_name")]
    public string FullName { get; set; } = null!;

    [JsonProperty("username")]
    public string Username { get; set; } = null!;

    [JsonProperty("password_hash")]
    public string PasswordHash { get; set; } = null!;

    [JsonProperty("password_salt")]
    public string PasswordSalt { get; set; } = null!;

    public Organizer ToDomainObject()
    {
        var id = OrganizerId.Create(Id).Value;
        var fullName = Domain.Organizers.FullName.Create(FullName).Value;
        
        var username = Domain.Organizers.Username.Create(Username).Value;
        var password = HashedPassword.Create(PasswordHash, PasswordSalt).Value;
        var credentials = new Credentials(username, password);
        
        var organizer = Organizer.Create(id, fullName, credentials).Value;

        return organizer;
    }

    public static OrganizerDocument Create(Organizer domainObject) =>
        new()
        {
            Id = domainObject.Id.Value,
            FullName = domainObject.FullName.Value,
            Username = domainObject.Credentials.Username.Value,
            PasswordHash = domainObject.Credentials.Password.Value,
            PasswordSalt = domainObject.Credentials.Password.Salt
        };
}