using AgendaApi.Interface.Data;
using AgendaApi.Interface.Service;
using AgendaApi.Logging;
using AgendaApi.Model;
using System.Text.Json;

namespace AgendaApi.Service
{
    public class AgendaService : IAgendaService
    {
        private readonly string _path = $"{Environment.CurrentDirectory}\\Data\\AgendaData.json";
        private readonly IAgendaRepository _repository;
        private readonly ILogManager _logger;

        public AgendaService(ILogManager logger, IAgendaRepository repository)
        {
            _logger = logger;
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
            IEnumerable<Contact> contactList = await GetAllAsync();

            if (contactList == null)
            {
                _logger.LogError("Lista de contactos vacia");
                throw new Exception("Lista de contactos vacia");
            }

            return contactList.Any(c => c.Id == id) ? contactList.FirstOrDefault(c => c.Id == id) : throw new Exception($"Este contacto no existe");
        }

        private async Task<bool> CreateAsync(IEnumerable<Contact> contactList)
        {
            return await _repository.WriteAsync(contactList);
        }

        private async Task<Guid> CreateAsync(Contact newContact)
        {
            List<Contact> contactList = (await GetAllAsync()).ToList();
            contactList.Add(newContact);

            if (await CreateAsync(contactList))
                return newContact.Id;

            return Guid.Empty;
        }

        private async Task<bool> DeleteAsync(Guid id)
        {
            List<Contact> contactList = (await GetAllAsync()).ToList();
            contactList.RemoveAll(c => c.Id == id);

            return await CreateAsync(contactList);
        }

        private async Task<Contact> UpdateAsync(Guid id, Contact newContact)
        {
            newContact.Id = id;
            List<Contact> contactList = (await GetAllAsync()).ToList();
            int indexOldContact = contactList.FindIndex(c => c.Id == id);
            contactList[indexOldContact] = newContact;
            await CreateAsync(contactList);

            return newContact;
        }
    }
}
