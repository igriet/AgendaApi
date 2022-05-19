namespace AgendaApi.Interface.Data
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> ReadAsync();
        Task<bool> Write(IEnumerable<T> newCollection);
    }
}
