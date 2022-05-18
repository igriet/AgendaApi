using System.Text.Json;

namespace AgendaApi.Model
{
    public class Contact
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class Agenda
    {
        public List<Contact> Contacts { get; set; }
    }
}

