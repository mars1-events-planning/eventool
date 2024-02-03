using System.Collections;

namespace Eventool.Domain.Organizers;

public class ContactsList : IEnumerable<Contact>
{
    private readonly HashSet<Contact> _contacts = [];

    public void AddContact(Contact contact) => _contacts.Add(contact);

    public void RemoveContact(Contact contact) => _contacts.Remove(contact);
    
    public IEnumerator<Contact> GetEnumerator() => _contacts.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_contacts).GetEnumerator();
}