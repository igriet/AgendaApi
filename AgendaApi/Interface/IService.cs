using AgendaApi.Model;

namespace AgendaApi.Interface
{
    public interface IService
    {
        Task<IEnumerable<Contact>> Read();

        Task<Contact> Get(Guid id);

        Task<Guid> Create(Contact newContact);

        Task<Contact> Update(Guid id, Contact contactUpdated);

        Task<bool> Delete(Guid id);
    }
}
