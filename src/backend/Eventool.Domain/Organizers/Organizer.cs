using System.Net.Mail;
using Eventool.Domain.Abstractions;

namespace Eventool.Domain.Organizers;

public class Organizer : Entity, IAggregateRoot
{
    public Organizer(
        Guid id,
        DateTime createdAtUtc,
        string fullName,
        MailAddress email,
        Password password)
    {
        Id = id;
        CreatedAtUtc = createdAtUtc;
        FullName = fullName;
        Email = email;
        Password = password;
    }

    public DateTime CreatedAtUtc { get; private set; }

    public string FullName { get; private set; }

    public Uri? PictureUri { get; init; }

    public MailAddress Email { get; private set; }

    public Password Password { get; private set; }

    public ContactsList Contacts { get; } = [];
}