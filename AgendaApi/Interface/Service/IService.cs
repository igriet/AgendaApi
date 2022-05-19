using AgendaApi.Model;

namespace AgendaApi.Interface.Service
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(Guid id);

        Task<Guid> Create(T newContact);

        Task<T> Update(Guid id, T contactUpdated);

        Task<bool> Delete(Guid id);
    }
}
