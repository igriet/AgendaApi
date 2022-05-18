using AgendaApi.Interface;
using AgendaApi.Model;
using System.Text.Json;

namespace AgendaApi.Service
{
    public class ContactService : IService
    {
        private readonly string _path = $"{Environment.CurrentDirectory}\\Data\\AgendaData.json";
        

        public async Task<Guid> Create(Contact newContact)
        {
            var getContactsTask = Read();

            await Task.WhenAll(getContactsTask);

            var contactList = getContactsTask.Result.ToList();
            contactList.Add(newContact);
            Agenda newAgenda = new Agenda();
            newAgenda.Contacts = contactList;
            string newJsonAgenda = JsonSerializer.Serialize<Agenda>(newAgenda);
            File.WriteAllText(_path, newJsonAgenda);
            return newContact.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            var getContactsTask = Read();
            await Task.WhenAll(getContactsTask);

            var contactList = getContactsTask.Result.ToList();
            contactList.RemoveAll(c => c.Id == id);
            Agenda newAgenda = new Agenda();
            newAgenda.Contacts = contactList;
            string newJsonAgenda = JsonSerializer.Serialize<Agenda>(newAgenda);
            File.WriteAllText(_path, newJsonAgenda);

            return true;
        }

        public async Task<Contact> Get(Guid id)
        {
            var getContactsTask = Read();
            await Task.WhenAll(getContactsTask);

            return getContactsTask.Result.FirstOrDefault(c => c.Id == id);
        }

        public async Task<IEnumerable<Contact>> Read()
        {
            using FileStream jsonStream = new FileStream(_path, FileMode.Open);
            Agenda agenda = await JsonSerializer.DeserializeAsync<Agenda>(jsonStream);

            return agenda.Contacts;
        }

        public async Task<Contact> Update(Guid id, Contact contactUpdated)
        {
            var getContactsTask = Read();
            await Task.WhenAll(getContactsTask);

            var contactList = getContactsTask.Result.ToList();
           
            var contact = contactList.FirstOrDefault(c => c.Id == id);

            contactList.RemoveAll(c => c.Id == id);
                        
            contact.Name = contactUpdated.Name;
            contact.Phone = contactUpdated.Phone;
            contact.Email = contactUpdated.Email;
            contactList.Add(contact);

            Agenda newAgenda = new Agenda();
            newAgenda.Contacts = contactList;

            string newJsonAgenda = JsonSerializer.Serialize<Agenda>(newAgenda);
            File.WriteAllText(_path, newJsonAgenda);

            return contact;
        }
    }
}
