using AgendaApi.Interface.Data;
using AgendaApi.Interface.Service;
using AgendaApi.Model;
using System.Text.Json;

namespace AgendaApi.Service
{
    public class AgendaService : IAgendaService
    {
        private readonly string _path = $"{Environment.CurrentDirectory}\\Data\\AgendaData.json";
        private readonly IAgendaRepository _repository;

        public AgendaService(IAgendaRepository repository)
        {
            _repository = repository;            
        }

        public async Task<Guid> Create(Contact newContact)
        {
            return await CreateAsync(newContact);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await DeleteAsync(id);
        }

        public async Task<Contact> Get(Guid id)
        {
            return await GetAsync(id);
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await GetAllAsync();
        }

        public async Task<Contact> Update(Guid id, Contact contactUpdated)
        {
            return await UpdateAsync(id, contactUpdated);
        }

        private async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _repository.ReadAsync();
        }

        private async Task<Contact> GetAsync(Guid id)
        {
            Task<IEnumerable<Contact>> getContactsTask = GetAllAsync();
            IEnumerable<Contact> contactList = await getContactsTask;

            return contactList.FirstOrDefault(c => c.Id == id);
        }

        private async Task<bool> CreateAsync(IEnumerable<Contact> contactList)
        {
            return await _repository.Write(contactList);
        }

        private async Task<Guid> CreateAsync(Contact newContact)
        {
            Task<IEnumerable<Contact>> getContactsTask = GetAllAsync();
            List<Contact> contactList = (await getContactsTask).ToList();
            contactList.Add(newContact);

            if (await CreateAsync(contactList))
                return newContact.Id;

            return Guid.Empty;
        }

        private async Task<bool> DeleteAsync(Guid id)
        {
            Task<IEnumerable<Contact>> getContactsTask = GetAllAsync();
            List<Contact> contactList = (await getContactsTask).ToList();
            contactList.RemoveAll(c => c.Id == id);

            return await CreateAsync(contactList);
        }

        private async Task<Contact> UpdateAsync(Guid id, Contact newContact)
        {
            newContact.Id = id;
            Task<IEnumerable<Contact>> getContactsTask = GetAllAsync();
            List<Contact> contactList = (await getContactsTask).ToList();
            int indexOldContact = contactList.ToList().FindIndex(c => c.Id == id);
            contactList[indexOldContact] = newContact;
            await CreateAsync(contactList);

            return newContact;
        }
    }
}
