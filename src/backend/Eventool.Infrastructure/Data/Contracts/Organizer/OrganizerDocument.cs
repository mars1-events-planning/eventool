using System.Net.Mail;
using Eventool.Domain.Organizers;
using MongoDB.Bson.Serialization.Attributes;

namespace Eventool.Infrastructure.Data;

[BsonCollection("organizers")]
public class OrganizerDocument(Organizer organizer) : IDocument<Organizer>
{
    [BsonId] // This attribute marks the property as the document's primary key.
    public Guid Id { get; set; } = organizer.Id;

    [BsonElement("fullName")] // This attribute represents the BSON field name.
    public string FullName { get; set; } = organizer.FullName;

    [BsonElement("email")]
    public string Email { get; set; } = organizer.Email.Address;

    [BsonElement("createdAtUtc")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)] // This ensures that the DateTime is handled as UTC.
    public DateTime CreatedAtUtc { get; set; } = organizer.CreatedAtUtc;

    [BsonElement("passwordHash")]
    public string PasswordHash { get; set; } = organizer.Password.Hash;

    [BsonElement("passwordSalt")]
    public string PasswordSalt { get; set; } = organizer.Password.Salt;

    [BsonElement("contacts")]
    public List<ContactDocument> Contacts { get; set; } =
        [..organizer.Contacts.Select(c => new ContactDocument(c))];
    
    
    public Organizer ToAggregate()
    {
        var organizer = new Organizer(
            Id, CreatedAtUtc, FullName, new MailAddress(Email), new Password(PasswordHash, PasswordSalt));

        foreach (var contact in Contacts)
            organizer.Contacts.AddContact(contact.ToAggregate());

        return organizer;
    }
}