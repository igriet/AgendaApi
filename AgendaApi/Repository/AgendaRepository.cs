using AgendaApi.Interface.Data;
using AgendaApi.Model;
using System.Text.Json;

namespace AgendaApi.Repository
{
    public class AgendaRepository : IAgendaRepository
    {
        private readonly string _path = $"{Environment.CurrentDirectory}\\Data\\AgendaData.json";

        public async Task<IEnumerable<Contact>> ReadAsync()
        {
            using FileStream jsonStream = new FileStream(_path, FileMode.Open);
            IEnumerable<Contact> agenda = await JsonSerializer.DeserializeAsync<IEnumerable<Contact>>(jsonStream);

            return agenda;
        }

        public async Task<bool> Write(IEnumerable<Contact> newCollection)
        {
            string newJsonAgenda = JsonSerializer.Serialize<IEnumerable<Contact>>(newCollection);
            await File.WriteAllTextAsync(_path, newJsonAgenda);

            return true;
        }
    }
}
